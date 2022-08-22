using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TL;
using WTelegram;

namespace MultipleAutoInviterMembersInGroup
{
    class AllLogic
    {
        public IList<string> GetListWithIdAndHashMembers(string path)
        {
            var list = new List<string>();
            using (StreamReader reader = new StreamReader(path))
            {
                string? line;

                while ((line = reader.ReadLine()) != null)
                {
                    list.Add(line);
                }

                return list;
            }
        }

        public IList<string> CreatedThirtyMembersFromListAndSavingBigListInFile(string path)
        {
            var list = GetListWithIdAndHashMembers(path).ToArray();

            using var writer = new StreamWriter(path, false);

            foreach (var item in list.Skip(30))
            {
                writer.WriteLine(item);
            }

            return list.Take(30).ToList();
        }

        public IDictionary<long, long> TransformListInDictionary(IList<string> list)
        {
            var result = new Dictionary<long, long>();
            foreach (var member in list)
            {
                var idx = member.IndexOf(':');

                var key = member.Substring(0, idx);
                var value = member.Substring(idx + 1);

                result.Add(Convert.ToInt64(key), Convert.ToInt64(value));

            }
            return result;
        }

        public string DataSearchForConfig(string a)
        {
            Console.Write("Enter path in folder with info for Config: ");
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
                continue;
            }
            return data;
        }
    }
}
