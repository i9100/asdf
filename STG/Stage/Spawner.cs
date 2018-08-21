using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace STG.Stage
{
    class Spawner
    {
        private static void InvokeMethod(string method, object[] args)
        {
            MethodInfo methodInfo = typeof(Entity.Enemy).GetMethod(method, BindingFlags.Public | BindingFlags.Static);
            methodInfo.Invoke(null, args);
        }

        private static IEnumerable<Schema> Parse(string document)
        {
            var result = JsonConvert.DeserializeObject<IEnumerable<Schema>>(document);
            return result;
        }

        public static async void Update(int stageNumber)
        {
            IEnumerable<Schema> stage = Parse(StageDocument.one);
            foreach(var stageData in stage)
            {
                object[] args = { stageData.Position };

                await Task.Delay(stageData.Delay);

                InvokeMethod(stageData.Name, args);
            }
        }
    }
}
