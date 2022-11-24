namespace backend.Models
{
    public enum SystemRole
    {
        Admin = 0,
        Trainer = 1,
        Trainee = 2,
    }

    public enum TraineeLearningState
    {
        InProgress = 0,
        Pass = 1,
        Fail = 2
    }
}
