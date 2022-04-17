namespace ApiTeam.Services
{
    public class CheckNameTeam
    {
        public static bool CheckName(string name, TeamService teamService)
        {
            if (teamService.GetName(name) != null)
            { return false; }
            else
            { return true; }
        }
    }
}
