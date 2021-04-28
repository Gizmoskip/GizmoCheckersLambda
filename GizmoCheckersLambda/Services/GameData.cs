/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GizmoCheckersLambda.Dynamo;

namespace GizmoCheckersLambda.Services
{
    public class GameData : IGameData
    {
        private readonly Dictionary<int, string> GameDataDict= new Dictionary<int, string>();
        private readonly IDynamoDB _dynamoDb;

        public GameData(IDynamoDB dynamoClient)
        {
            _dynamoDb = dynamoClient;
        }
        public Dictionary<int, string> GetGameDataFull()
        {
            return GameDataDict;
        }
        public string GetGameData(int id)

        {
            return GameDataDict[id];
        }
        public void AddGameData(int id, string data)

        {
            try
            {
                if (GameDataDict.ContainsKey(id))
                {
                    GameDataDict.Remove(id);
                    GameDataDict.Add(id, data);
                }
                else
                {
                    GameDataDict.Add(id, data);
                }
            }
            catch (Exception e)
            {
                GameDataDict.Add(id, data);
            };
            
            
        }

        public string CreateGame()
        {
            var randS = new Random();
            int randomNumber = randS.Next();
            _dynamoDb.NewGame(randomNumber, "112114116118121123125127132134136138287285283281272274276278267265263261");
            return randomNumber.ToString();
        }



    }
}
*/