/// <summary>
/// Project:    SIT323 - Practical Software Development - Assignmnet 2
/// Written By: Chris O'Beirne - Student #211347444
/// Date:       02/10/16
/// </summary>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Timers;

namespace CrozzleGame.Models
{
    public class CrozzleSolverModel
    {
        #region Class Constants

        const double TickInterval = 1000;
        public const double TimeoutInterval = 300000;

        #endregion

        #region Class Events
        /// <summary>
        /// This event is triggerd with the solver update interval timer activates.
        /// </summary>
        public event SolverUpdateHandler SolverUpdate;
        public delegate void SolverUpdateHandler(CrozzleSolverModel solver, EventArgs e);

        #endregion

        #region Class Fields

        /// <summary>
        /// This timer activates at set intervals to trigger an update event.
        /// </summary>
        private System.Timers.Timer TickTimer;

        /// <summary>
        /// This timer activates to interupt and force the solver to complete after a timeout 
        /// interval.
        /// </summary>
        private System.Timers.Timer TimeoutTimer;

        /// <summary>
        /// A signal for the solver to finish.
        /// </summary>
        private bool SolutionTimeout = false;

        /// <summary>
        /// This time remaining before the solver timeout occurs.
        /// </summary>
        private DateTime SolverTimeoutTime;

        #endregion

        #region Class Properties

        /// <summary>
        /// The current crozzle to be solved.
        /// </summary>
        public CrozzleModel Crozzle { get; set; }

        /// <summary>
        /// The size of the current word pool to be solved.
        /// </summary>
        public int WordCount { get; set; }

        /// <summary>
        /// The number of words used in the best solution.
        /// </summary>
        public int WordsSolved { get; set; }

        /// <summary>
        /// The solvers word pool.
        /// </summary>
        public Dictionary<string, int> WordPool { get; set; }

        /// <summary>
        /// The subsolutions in the best solution.
        /// </summary>
        public List<SubSolutionModel> SubSolutions { get; set; }

        /// <summary>
        /// The best solution grid.
        /// </summary>
        public GridModel SolutionGrid { get; set; }

        /// <summary>
        /// The current best solution node.
        /// </summary>
        public SolutionNode BestSolutionNode { get; set; }

        /// <summary>
        /// The time remaining before the solver is interupted and ends.
        /// </summary>
        public double TimeRemaining { get; set; }

        #endregion

        #region Class Constructors

        /// <summary>
        /// Crozzle Solver Model constructor, is called on object creation and initialises object 
        /// properties.
        /// </summary>
        /// <param name="crozzle">The crozzle to be solved.</param>
        public CrozzleSolverModel(CrozzleModel crozzle)
        {
            this.Crozzle = crozzle;
            this.WordPool = new Dictionary<string, int>();
            this.SubSolutions = new List<SubSolutionModel>();
            this.SolutionGrid = new GridModel(crozzle);
            this.BestSolutionNode = new SolutionNode();
            this.TickTimer = new System.Timers.Timer(TickInterval);
            this.TimeoutTimer = new System.Timers.Timer(TimeoutInterval);
        }

        #endregion

        #region Event Handlers

        /// <summary>
        /// This handler is triggered each time the Tick Timer elapses. The Time Remaining property
        /// is updated and then Solver Update Event is triggered. This updates the GUI solver 
        /// status.
        /// </summary>
        /// <param name="source">The timer the triggerd the event.</param>
        /// <param name="e">The timer event arguments.</param>
        private void OnTickEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            this.TimeRemaining = (this.SolverTimeoutTime - DateTime.Now).TotalSeconds;

            if (this.TimeRemaining < 0)
            {
                this.TimeRemaining = 0;
            }

            SolverUpdate(this, e);
        }

        /// <summary>
        /// This handler is triggered once the solver has reached its timeout interval. A signal 
        /// is set and consequently the Greedy Algorithm finishes at the current node.
        /// </summary>
        /// <param name="source">The timer the triggerd the event.</param>
        /// <param name="e">The timer event arguments.</param>
        private void OnTimeoutEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            SolutionTimeout = true;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// This function attempts to solve the crozzle.  The word pool is pre-processed depending
        /// on the crozzle difficulty level to find sub solutions. The sub solutions are then 
        /// processed by a Greedy Algorithm to find the best solution.
        /// The solver will complete after the algorithm has exhauseted all possibilities or the 
        /// timeout interval has elapsed.
        /// </summary>
        /// <param name="enableEvents">Enable the solver event timers.</param>
        public void SolveCrozzle(bool enableEvents = true)
        {
            // Enable the event timers - can be disabled for unit testing.
            if (enableEvents)
            {
                InitialiseEventTimers();
            }

            // Initialise the word count for GUI status.
            this.WordCount = this.Crozzle.WordPool.Count;

            // Score the word pool based on the current configuration.
            ScoreWordPoolByIntersect();

            // Find sub solutions based on configuration difficulty.
            switch (this.Crozzle.Difficulty)
            {
                case "EASY":
                    {          
                        FindEasySubSolutions();
                    }
                    break;

                case "MEDIUM":
                    {
                        FindMediumSubSolutions();
                    }
                    break;

                case "HARD":
                    {
                        // Add easy pre processed solutions first, this improves the overall score.
                        FindEasySubSolutions();
                        FindHardSubSolutions();
                    }
                    break;
            }
            
            // Initialise the solution node tree.
            SolutionNode rootNode = new SolutionNode();
            rootNode.NodeGrid = new GridModel(this.Crozzle);
            rootNode.SubSolutions = this.SubSolutions.ToList();

            // Call recursive greedy algorithm to find the best solution.
            InsertChildren(rootNode);

            // Disable the event timers prior to exit.
            TickTimer.Enabled = false;
            TimeoutTimer.Enabled = false;
        }

        #endregion

        #region Private Methods


        /// <summary>
        /// This function sets the presets and starts the event timers. The Tick Timer is used to
        /// trigger status updates i.e. on the GUI. The Timeout Timer is used to interupt the 
        /// solver once the timeout period has elapsed.
        /// </summary>
        private void InitialiseEventTimers()
        {
            // Initilise and enable a self resetting timer.
            TickTimer.Elapsed += OnTickEvent;
            TickTimer.AutoReset = true;
            TickTimer.Enabled = true;

            // Initialise and enable a one shot timer.
            TimeoutTimer.Elapsed += OnTimeoutEvent;
            TimeoutTimer.AutoReset = false;
            TimeoutTimer.Enabled = true;

            // Update the expected solver timeout time.
            SolverTimeoutTime = DateTime.Now.AddMilliseconds(TimeoutInterval);
        }

        /// <summary>
        /// This function takes a primary word and attempts to build a (2x2) cluster of 
        /// intersecting words from the word pool. Each cluster has four words.  Clusters that
        /// extend out of grid bounds are ignored. Cluster variants are created from each adjacent 
        /// pair of the first word.
        /// </summary>
        /// <param name="workingWordPool">The pool of available words.</param>
        /// <param name="firstWord">The first vertical word of the cluster.</param>
        /// <param name="primaryPairIndex">Index of the pair (L) to be clustered.</param>
        private void FindClusterSubSolution(List<string> workingWordPool, string firstWord,
            int primaryPairIndex)
        {
            const int SecondLetterOffset = 1;
            const int CharacterArrayOffset = 1;
            const string VerticalOrientation = "VERTICAL";
            const string HorizontalOrientation = "HORIZONTAL";
            const int GridIndexOffset = 1;

            // Get the pair (P).
            char primaryPairFirstLetter = firstWord[primaryPairIndex];
            char primaryPairSecondLetter =
                firstWord[primaryPairIndex + SecondLetterOffset];

            try
            {
                // Find the second horizontal intersecting word for the first letter of the
                // pair (P).
                string secondWord =
                    workingWordPool.First(w => w.Contains(primaryPairFirstLetter));
                workingWordPool.Remove(secondWord);

                // Find second word intersect index.
                int secondWordIntersectIndex = secondWord.IndexOf(primaryPairFirstLetter);

                try
                {
                    // Find the third horizontal intersecting word for the second letter of
                    // the pair (P).
                    string thirdWord =
                        workingWordPool.First(w => w.Contains(primaryPairSecondLetter));
                    workingWordPool.Remove(thirdWord);

                    // Find third word intersect index.
                    int thirdWordIntersectIndex =
                        thirdWord.IndexOf(primaryPairSecondLetter);

                    // Check for adjacent pair (L) to the left of (P).
                    bool pairLeftExists = false;
                    if ((secondWordIntersectIndex > CharacterArrayOffset) &&
                        (thirdWordIntersectIndex > CharacterArrayOffset))
                    {
                        pairLeftExists = true;
                    }

                    // Check for adjacent pair (R) to the right of (P).
                    bool pairRightExists = false;
                    if ((secondWordIntersectIndex <
                        (secondWord.Length - CharacterArrayOffset)) &&
                        (thirdWordIntersectIndex <
                        (thirdWord.Length - CharacterArrayOffset)))
                    {
                        pairRightExists = true;
                    }

                    // If there is not an adjacent pair (L) and (R).
                    if (!(pairLeftExists && pairRightExists))
                    {
                        // Get the fourth word sub string.
                        string fourthSubString = string.Empty;

                        // If a pair (L) exists, get sub string (L).                
                        if (pairLeftExists)
                        {
                            fourthSubString =
                                string.Concat(secondWord[secondWordIntersectIndex -
                                CharacterArrayOffset], thirdWord[thirdWordIntersectIndex -
                                CharacterArrayOffset]);
                        }

                        // If a pair (R) exists, get sub string (R).
                        if (pairRightExists)
                        {
                            fourthSubString =
                                string.Concat(secondWord[secondWordIntersectIndex +
                                CharacterArrayOffset], thirdWord[thirdWordIntersectIndex +
                                CharacterArrayOffset]);
                        }

                        try
                        {
                            // Return a pool of fourth word candidates.
                            List<string> fourthWordPool =
                                workingWordPool.Where(w => w.Contains(fourthSubString)).ToList();

                            // Attempt to create a sub solution using all candidates.
                            foreach (string fourthWord in fourthWordPool)
                            {
                                // Find fourth word intersect index.
                                int fourthWordIntersectIndex =
                                    fourthWord.IndexOf(fourthSubString);

                                // Calculate the word start rows.
                                int firstWordStartRow =
                                    fourthWordIntersectIndex + GridIndexOffset;
                                int secondWordStartRow =
                                    firstWordStartRow + primaryPairIndex;
                                int thirdWordStartRow =
                                    firstWordStartRow + primaryPairIndex + GridIndexOffset;
                                int fourthWordStartRow = GridIndexOffset;

                                // Find the left most second or third word, then calculate 
                                // word start columns.
                                int firstWordStartColumn;
                                int secondWordStartColumn;
                                int thirdWordStartColumn;
                                int fourthWordStartColumn;

                                // Calculate the word start columns.
                                if (secondWordIntersectIndex > thirdWordIntersectIndex)
                                {
                                    secondWordStartColumn = GridIndexOffset;
                                    firstWordStartColumn =
                                        secondWordIntersectIndex + GridIndexOffset;
                                    thirdWordStartColumn =
                                        firstWordStartColumn - thirdWordIntersectIndex;
                                    fourthWordStartColumn =
                                        firstWordStartColumn + GridIndexOffset;
                                }
                                else
                                {
                                    thirdWordStartColumn = GridIndexOffset;
                                    firstWordStartColumn =
                                        thirdWordStartColumn + GridIndexOffset;
                                    secondWordStartColumn =
                                        firstWordStartColumn - secondWordIntersectIndex;
                                    fourthWordStartColumn =
                                        firstWordStartColumn + GridIndexOffset;
                                }

                                // Calculate the word end rows.
                                int firstWordEndRow = firstWordStartRow +
                                    firstWord.Length - GridIndexOffset;
                                int secondWordEndRow = secondWordStartRow;
                                int thirdWordEndRow = thirdWordStartRow;
                                int fourthWordEndRow = fourthWordStartRow +
                                    fourthWord.Length - GridIndexOffset;

                                // Calculate the word end columns.
                                int firstWordEndColumn = firstWordStartColumn;
                                int secondWordEndColumn = secondWordStartColumn +
                                    secondWord.Length - GridIndexOffset;
                                int thirdWordEndColumn = thirdWordStartColumn +
                                    thirdWord.Length - GridIndexOffset;
                                int fourthWordEndColumn = fourthWordStartColumn;

                                // Check for end rows out of bounds.
                                bool rowOutOfBounds = false;
                                if ((firstWordEndRow > this.Crozzle.Rows) ||
                                    (secondWordEndRow > this.Crozzle.Rows) ||
                                    (thirdWordEndRow > this.Crozzle.Rows) ||
                                    (fourthWordEndRow > this.Crozzle.Rows))
                                {
                                    rowOutOfBounds = true;
                                }

                                // Check for end columns out of bounds.
                                bool columnOutOfBounds = false;
                                if ((firstWordEndColumn > this.Crozzle.Columns) ||
                                    (secondWordEndColumn > this.Crozzle.Columns) ||
                                    (thirdWordEndColumn > this.Crozzle.Columns) ||
                                    (fourthWordEndColumn > this.Crozzle.Columns))
                                {
                                    columnOutOfBounds = true;
                                }

                                if (!(rowOutOfBounds || columnOutOfBounds))
                                {
                                    // Create sub solution using all four words.
                                    SubSolutionModel subSolution = new SubSolutionModel();
                                    subSolution.GroupWords.Add(
                                        new WordModel(VerticalOrientation,
                                        firstWordStartRow, firstWordStartColumn,
                                        firstWord));
                                    subSolution.GroupWords.Add(
                                        new WordModel(HorizontalOrientation,
                                        secondWordStartRow, secondWordStartColumn,
                                        secondWord));
                                    subSolution.GroupWords.Add(
                                        new WordModel(HorizontalOrientation,
                                        thirdWordStartRow, thirdWordStartColumn,
                                        thirdWord));
                                    subSolution.GroupWords.Add(
                                        new WordModel(VerticalOrientation,
                                        fourthWordStartRow, fourthWordStartColumn,
                                        fourthWord));

                                    // Create a grid using the sub solution.
                                    GridModel workingGrid =
                                        new GridModel(this.Crozzle.DeepCopy());
                                    workingGrid.InsertSubSolution(subSolution);

                                    // Validate the sub solution.
                                    CrozzleValidationModel subValidator =
                                        new CrozzleValidationModel(workingGrid);

                                    // If valid, add to sub solution collection.
                                    if (subValidator.CrozzleIsValid)
                                    {
                                        this.SubSolutions.Add(subSolution);
                                    }
                                }
                            }
                        }
                        catch (Exception)
                        { }
                    }
                }
                catch (Exception)
                { }
            }
            catch (Exception)
            { }
        }

        /// <summary>
        /// To acheive the highest score for the Easy Configuration, the maximum number of letters 
        /// must be placed in the grid - the number of words or intersects is not relevant. 
        /// Therefore sub solutions can be pre processed to influence the outcome of the greedy 
        /// algorithm.
        /// Fistly, create sub solutions using two words with matching first letters - prioritise
        /// the longest words.
        /// Secondly, repeat above - priorities the shortest words.
        /// NB: the second step is important as this ensures that a subset of short word solutions
        /// are tested in small grid areas.
        /// </summary>
        private void FindEasySubSolutions()
        {
            const int MaxLetterIndex = 0;

            // Match the highest scoring words first.
            List<string> rankedWords =
                this.WordPool.OrderByDescending(w => w.Value).Select(w => w.Key).ToList();

            this.SubSolutions.AddRange(FindPairSubSolution(MaxLetterIndex, rankedWords));

            // Match the lowest scoring words first.
            rankedWords.Clear();
            rankedWords =
                this.WordPool.OrderBy(w => w.Value).Select(w => w.Key).ToList();
            this.SubSolutions.AddRange(FindPairSubSolution(MaxLetterIndex, rankedWords));

            // Prioritise the highest scoring sub solutions.
            this.SubSolutions.OrderByDescending(s => s.Score).ToList();
        }

        /// <summary>
        /// To acheive the highest score for the Medium Configuration, as many of the highest 
        /// scoring letters should be used as possble.  Therefore sub solutions can be pre 
        /// processed to influence the outcome of the greedy algorithm, by matching word pairs 
        /// that return the highest individual score.  The interesection letter of each sub
        /// solution is not relevant.
        /// </summary>
        private void FindMediumSubSolutions()
        {
            // Rank words by their individual score.
            List<string> rankedWords =
                this.WordPool.OrderByDescending(w => w.Value).Select(w => w.Key).ToList();

            // Set the maximum intersection index to that of the longest word i.e. all words can 
            // intersect at any letter.
            int maxLetterIndex = rankedWords.Max(w => w.Length);

            // Find the sub solutions.
            this.SubSolutions = FindPairSubSolution(maxLetterIndex, rankedWords);
        }

        /// <summary>
        /// To acheive the highest score for the Hard Configuration, as many of the highest 
        /// scoring intersect letters and as many of the words should be used as possble.  
        /// Therefore pre processing can influence the outcome of the greedy algorithm, by finding
        /// words that intersect in (2x2) clusters.
        /// </summary>
        private void FindHardSubSolutions()
        {
            const int InitialPairIndex = 0;
            const int FirstWordLastPairOffset = 1;

            // Find the highest scoring words.
            List<string> rankedWords =
                this.WordPool.OrderBy(w => w.Value).Select(w => w.Key).ToList();

            // Iterate through the word pool.
            foreach (var currentWord in rankedWords)
            {
                // Create a working word pool, words will be removed as they are used.
                List<string> workingWordPool = rankedWords.Select(w => w).ToList();

                // Select the first vertical word.
                string firstWord = currentWord;
                workingWordPool.Remove(firstWord);

                // For each pair of letters in the first word (P).
                for (int primaryPairIndex = InitialPairIndex; primaryPairIndex < 
                    (firstWord.Length - FirstWordLastPairOffset); primaryPairIndex++)
                {
                    FindClusterSubSolution(workingWordPool, firstWord, primaryPairIndex);
                }
            }
        }

        /// <summary>
        /// This function iterates through a list of ranked words. The algorithm attempts to find a
        /// second word in the list that matches the current letter of the first word. If a match
        /// is not found the next letter in the first word is searched. This continues until there
        /// are no letters left or the maximum letter index is reached.
        /// </summary>
        /// <param name="maxLetterIndex">The pool of available words.</param>
        /// <param name="rankedWords">The first vertical word of the cluster.</param>
        private List<SubSolutionModel> FindPairSubSolution(int maxLetterIndex, List<string> rankedWords)
        {
            const int InitialLetterIndex = 0;
            const string PrimaryWordOrientation = "HORIZONTAL";
            const string SecondaryWordOrientation = "VERTICAL";
            const int PrimarWordStartColumn = 0;
            const int SecondaryWordStartRow = 0;
            const int CrozzleGridRowIndexOffset = 1;
            const int CrozzleGridColumnIndexOffset = 1;

            // Initialise sub solution collection.
            List<SubSolutionModel> returnSolutions = new List<SubSolutionModel>();

            // Initiliase a working list of ranked words.
            List<string> workingRankedWords = rankedWords.ToList();

            // Index through the ordered list of words (highest first).
            foreach (string rankedWord in rankedWords)
            {
                // Check for at least 2 working words and the current word is still a working word.
                if (workingRankedWords.Count > 1 && workingRankedWords.Contains(rankedWord))
                {
                    // Take the first word from the list (highest score).
                    string primaryWord = workingRankedWords[0];

                    // Begining with the highest score letter, find the next word that contains the 
                    // letter.
                    string secondaryWord = string.Empty;
                    int currentLetterIndex = InitialLetterIndex;

                    while ((currentLetterIndex < primaryWord.Length) && 
                        (currentLetterIndex <= maxLetterIndex))
                    {
                        char currentLetter = primaryWord[currentLetterIndex];

                        foreach (string word in rankedWords)
                        {
                            // Not the primary word, is in the working list and contains letter.
                            if ((word != primaryWord) &&
                                (workingRankedWords.Contains(word)) &&
                                (word.Contains(currentLetter.ToString())))
                            {
                                secondaryWord = word;

                                // Find the letter position in the first word.
                                int primaryIntersectIndex = primaryWord.IndexOf(currentLetter);

                                // Find the letter position in the second word.
                                int secondaryIntersectIndex = secondaryWord.IndexOf(currentLetter);

                                if ((primaryIntersectIndex <= maxLetterIndex) && 
                                    (secondaryIntersectIndex <= maxLetterIndex))
                                {
                                    // Create a sub-solution.
                                    SubSolutionModel firstSubSolution = new SubSolutionModel();

                                    // Insert the first word, offset row by second word intersect 
                                    // position.
                                    firstSubSolution.GroupWords.Add(
                                        new WordModel(PrimaryWordOrientation,
                                        secondaryIntersectIndex + CrozzleGridRowIndexOffset,
                                        PrimarWordStartColumn + CrozzleGridColumnIndexOffset,
                                        primaryWord));

                                    // Insert the second word, offset column by the first word 
                                    // intersect position.
                                    firstSubSolution.GroupWords.Add(
                                        new WordModel(SecondaryWordOrientation,
                                        SecondaryWordStartRow + CrozzleGridRowIndexOffset,
                                        primaryIntersectIndex + CrozzleGridColumnIndexOffset,
                                        secondaryWord));

                                    // Add grid to sub-solutions collection.
                                    returnSolutions.Add(firstSubSolution);
                                                                                                          
                                    // Dont check the primary word again.
                                    workingRankedWords.Remove(primaryWord);

                                    // Dont check the secondary word again.
                                    workingRankedWords.Remove(secondaryWord);
                                }

                                // Initialise the secondary word for the next iteration.
                                secondaryWord = string.Empty;
                            }
                        }

                        currentLetterIndex++;
                    }
                }
            }

            return returnSolutions;
        }
        
        /// <summary>
        /// This is a recursive function used to build a tree of solution nodes. Beginning with 
        /// the root node, a collection of "next best" sub solutions is found. For each sub
        /// solution a child is created by copying the current node and adding the remaining sub 
        /// solutions as children. The child node is linked to the current node and a recursive 
        /// call is made to this function to find the children of the new child node. The cycle 
        /// then repeats, building a node tree of possible solutions.
        /// Once the branch has been fully traversed i.e. there are no more possible solutions, the 
        /// current node score is checked against the best node score. If a new high score has been
        /// found the best node is updated.
        /// The algorithm will complete once all branches have been traversed or the timeout 
        /// sentinel has activated.
        /// </summary>
        /// <param name="node">The node that requires children.</param>
        private void InsertChildren(SolutionNode node)
        {
            // Find the best subsolutions for the current node.
            List<SubSolutionModel> subSolutions = 
                GetBestSubsolutions(node.NodeGrid, node.SubSolutions);

            if(subSolutions.Count == 0)
            {
                // No sub solutions, the end of the branch has been reached.
                // Check if the best score has been beaten.
                if (node.NodeScore > this.BestSolutionNode.NodeScore)
                {
                    // The current branch has returned the best score.
                    this.BestSolutionNode = node.DeepCopy();
                    this.WordsSolved = node.NodeGrid.Crozzle.WordList.Count;
                }
            }
            else
            {
                // Create a new child for each sub solution.
                foreach(SubSolutionModel subSolution in subSolutions)
                {
                    // Copy the node and insert sub solution.
                    SolutionNode childNode = node.DeepCopy();
                    childNode.NodeGrid.InsertSubSolution(subSolution);

                    // Score the node.
                    ScoreModel nodeScore = new ScoreModel(childNode.NodeGrid, true);
                    childNode.NodeScore = nodeScore.TotalScore;

                    // Dont use the sub solution again.
                    childNode.SubSolutions.RemoveAll(s => s.MatchSubSolutionWords(subSolution));

                    // Make this node the parent of the new child.
                    node.ChildNodes.Add(childNode);

                    // Check if we still have time left.
                    if (!SolutionTimeout)
                    {
                        // Recursive call to find children for the current node.
                        InsertChildren(childNode);
                    }
                }
            }
        }
        
        /// <summary>
        /// This function takes the current grid and then attempts to find any sub solutions 
        /// that can be added without invalidating the grid. This involves overlaying each sub 
        /// solution into every position within the bounds of the grid.
        /// Each valid solution is scored and shortlisted. Any solutions equaling the highest 
        /// score are returned as the best sub solutions.
        /// </summary>
        /// <param name="currentGrid">The current grid.</param>
        /// <param name="subSolutions">The pre processed sub solutions.</param>
        /// <returns></returns>
        private List<SubSolutionModel> GetBestSubsolutions(GridModel currentGrid, 
            List<SubSolutionModel> subSolutions)
        {
            const int StartRow = 0;
            const int StartColumn = 0;

            List<SubSolutionModel> bestSubSolutions = new List<SubSolutionModel>();

            // Create a working copy of the sub solutions.
            List<SubSolutionModel> workingSubSolutions = subSolutions.ToList();
            
            // For every sub solution.
            foreach(SubSolutionModel subSolution in workingSubSolutions)
            {
                // Iterate through each row.
                for (int rowOffset = StartRow; rowOffset < this.Crozzle.Rows; rowOffset++)
                {
                    // Iterate through each column.
                    for (int columnOffset = StartColumn; columnOffset < 
                        this.Crozzle.Columns; columnOffset++)
                    {
                        // Create a working grid and attempt to insert the subsolution.
                        GridModel workingGrid = new GridModel(currentGrid.Crozzle.DeepCopy());
                        workingGrid = currentGrid.DeepCopy();

                        // Offset the sub solution based on the row\column starting index.
                        subSolution.ArrayRowffset = rowOffset;
                        subSolution.ArrayColumnOffset = columnOffset;

                        try
                        {
                            // Attempt to insert the solution into the working grid.
                            bool solutionInsertedOk = workingGrid.InsertSubSolution(subSolution);

                            // Only attempt validation if the working grid was changed.
                            if (solutionInsertedOk)
                            {
                                // Validate the sub solution.
                                CrozzleValidationModel subValidator = 
                                    new CrozzleValidationModel(workingGrid);

                                // Add to the best solutions shortlist
                                if (subValidator.CrozzleIsValid)
                                {
                                    // Score the sub solution.
                                    ScoreModel subScore = 
                                        new ScoreModel(workingGrid, subValidator.CrozzleIsValid);
                                    subSolution.Score = 
                                        subScore.CalculateSubSolutionScore(subSolution);

                                    // Add to shortlist of best sub solutions
                                    SubSolutionModel validSubSolution =
                                        new SubSolutionModel(subSolution, rowOffset, columnOffset);
                                    bestSubSolutions.Add(validSubSolution);
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            throw new Exception("Exception while inserting sub solution.", e);
                        }
                    }
                }
            }

            // Return the top scoring sub solutions.
            bestSubSolutions = bestSubSolutions.OrderByDescending(s => s.Score).ToList();
            bestSubSolutions = 
                bestSubSolutions.Where(s => s.Score == bestSubSolutions.First().Score).ToList();

            return bestSubSolutions;
        }

        /// <summary>
        /// This function scores each word in the word pool by intersecting letter score.
        /// </summary>
        private void ScoreWordPoolByIntersect()
        {
            const int InitialWordScore = 0;

            // Index through the word list.
            foreach (string word in this.Crozzle.WordPool)
            {
                int wordScore = InitialWordScore;

                // Find the intersecting score of each letter.
                foreach (char letter in word)
                {
                    wordScore +=
                        this.Crozzle.Configuration.IntersectingPoints[letter.ToString()];
                }

                // Update the word score.
                this.WordPool.Add(word, wordScore);
            }
        }

        #endregion
    }
}
