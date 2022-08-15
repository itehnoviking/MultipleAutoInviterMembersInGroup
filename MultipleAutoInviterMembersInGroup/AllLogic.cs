using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultipleAutoInviterMembersInGroup
{
    class AllLogic
    {
        public async IAsyncEnumerable<string> GetListWithIdAndHashMembersAsync(string path)
        {
            using var reader = new StreamReader(path);

            while (!reader.EndOfStream)
            {
                yield return await reader.ReadLineAsync();
            }
        }

        public async Task<IList<string>> CreatedThirtyMembersFromListAndSavingBigListInFileAsync(string path)
        {
            var thirtyMembers = new List<string>();
            var list = GetListWithIdAndHashMembersAsync(path);

            var listEnumerator = list.GetAsyncEnumerator();

            for (int i = 0; i < 30; i++)
            {
                if (!await listEnumerator.MoveNextAsync())
                {
                    break;
                }

                thirtyMembers.Add(listEnumerator.Current);
            }

            using var writer = new StreamWriter(path, false);
            while (await listEnumerator.MoveNextAsync())
            {
                await writer.WriteLineAsync(listEnumerator.Current);
            }
            return thirtyMembers;
        }

        public async Task<IDictionary<long, long>> TransformListInDictionary(IList<string> list)
        {
            var result = new Dictionary<long, long>();
            foreach (var member in list)
            {
                var idx = member.IndexOf(':');

                var key = member.Substring(0, idx);
                var value = member.Substring(idx + 1);

                // dictionary.TryAdd ?
                // long.TryParse(key) ?
                result.Add(Convert.ToInt64(key), Convert.ToInt64(value));
            }

            return result;
        }

        public string DataSearchForConfig(string a)
        {
            Console.Write("Enter path in folder: ");
            string path = Console.ReadLine();
            string data = null;

            var dir = new DirectoryInfo(path);
            foreach (var file in dir.GetFiles())
            {
                if (file.Name == a)
                {
                    using var reader = new StreamReader($"{file.FullName}");

                    while (!reader.EndOfStream)
                    {
                        data = reader.ReadLine();
                    }
                    break;
                }
                break;
            }
            return data;
        }
    }
}
