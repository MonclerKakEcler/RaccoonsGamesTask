namespace RaccoonsGames.GameProgressUI
{
    public class GameProgressModel
    {
        public int Value { get; private set; }
        public int AttemptCount { get; private set; } = 501;

        public int TargetScore { get; private set; } = 2048;

        public void AddScore(int amount)
        {
            Value += amount;
        }

        public void DecrementAttempts()
        {
            AttemptCount--;
        }

        public bool IsGoalReached()
        {
            return Value >= TargetScore;
        }

        public bool IsOutOfAttempts()
        {
            return AttemptCount <= 0;
        }
    }
}