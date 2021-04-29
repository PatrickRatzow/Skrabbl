using Skrabbl.Model;

namespace Skrabbl.DataAccess.Test.Util
{
    public class TestData
    {
        public struct Users
        {
            public static User Patrick { get; set; } = new User
            {
                Username = "Patrick",
                Email = "patrick@email.dk",
                Password = "pLNZSux4l2ar1z6PKh4tiBSZ25OSaim5R1bmXuD+aS8=",
                Salt = "mvV8K4PoKh41psKjxAWTGQ=="
            };

            public static User Nikolaj { get; set; } = new User
            {
                Username = "Nikolaj",
                Email = "nikolaj@email.dk",
                Password = "z7W5yuuDOuPNrkX8bYVFiWUSxwyJRfp4U4uEEGtDLn8=",
                Salt = "0ccvXWEEtGOmwtJRSEG1+g=="
            };

            public static User Floris { get; set; } = new User
            {
                Username = "Floris",
                Email = "floris@email.dk",
                Password = "z7W5yuuDOuPNrkX8bYVFiWUSxwyJRfp4U4uEEGtDLn8=",
                Salt = "0ccvXWEEtGOmwtJRSEG1+g=="
            };

            public static User Simon { get; set; } = new User
            {
                Username = "Simon",
                Email = "email@simon.dk",
                Password = "z7W5yuuDOuPNrkX8bYVFiWUSxwyJRfp4U4uEEGtDLn8=",
                Salt = "0ccvXWEEtGOmwtJRSEG1+g=="
            };
        }

        public struct GameLobbies
        {
            public static GameLobby PatrickLobby { get; set; } = new GameLobby
            {
                GameCode = "abcd"
            };

            public static GameLobby NikolajLobby { get; set; } = new GameLobby
            {
                GameCode = "dcba"
            };

            public static GameLobby? FlorisLobby { get; set; }
        }

        public struct Games
        {
            public static Game PatrickGame { get; } = new Game();
            public static Game NikolajGame { get; } = new Game();
            public static Game? FlorisGame { get; set; }
        }
    }
}