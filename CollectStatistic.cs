using System;
using System.Collections.Generic;
using System.Text;

using JSON_Data;
using JSON_Vacancy;

using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace HH
{
    class CollectStatistic
    {
        public HttpClient client;

        private int status = 4;

        List<Task> task = new List<Task>();
        List<CollectParam> professions = new List<CollectParam>();
        List<CollectParam> skills = new List<CollectParam>();

        private int allVacancies = 0;
        private int loadVacancies = 0;

        private int? salary_from = null;
        private int? salary_to = null;

        public int Salary_from
        {
            get {return salary_from.HasValue ? (int)salary_from : 0;}
            set {salary_from = (value <= 0) ? null : (int?)value;}
        }
        public int Salary_to
        {
            get { return salary_to.HasValue ? (int)salary_to : 0; }
            set { salary_to = (value <= 0) ? null : (int?)value; }
        }
        private int GetSalary
        {
            get
            {
                if ((!salary_from.HasValue) && (!salary_to.HasValue))
                    return 0;
                if ((!salary_from.HasValue))
                    return (int)salary_to;
                if (!salary_to.HasValue)
                    return (int)salary_from;
                return (int)(salary_from + salary_to) /2;
            }
        }
        public bool Finished
        {
            get { return status == 4; }
        }


        public async Task Start()
        {
            await Task.Delay(0).ConfigureAwait(false);

            status = 0;
            allVacancies = 0;
            loadVacancies = 0;

            professions.Clear();
            skills.Clear();
            task.Clear();

            string url = @"https://api.hh.ru/vacancies";
            string sub_url = @"?salary="+GetSalary+@"&only_with_salary=true";
            int page = 0;
            int per_page = 100;
            int pages = 0;
            List<string> vacancy_id = new List<string>();

            do
            {
                Data data = await GetData(url + sub_url + "&page=" + page + "&per_page=" + per_page);
                status = 1;
                foreach (var vacancy in data.Items)
                    if(ExeptVacancy((int?)vacancy.Salary.From, (int?)vacancy.Salary.To))
                        vacancy_id.Add(vacancy.Id);
                allVacancies = vacancy_id.Count;
                page = Convert.ToInt32(data.Page) + 1;
                pages = Convert.ToInt32(data.Pages);
            }
            while (page < pages);

            status = 2;
            foreach (var vacancy in vacancy_id)
            {
                task.Add(SubProgVacancy(url + "/" + vacancy));
                do
                {
                    for (int i = 0; i < task.Count; i++)
                        if (task[i].IsCompleted)
                            task.RemoveAt(i);
                }
                while (task.Count >= 30);
            }
            Task.WaitAll(task.ToArray());
            status = 3;
            SortLists();
            status = 4;
        }

        private async Task SubProgVacancy(string url)
        {
            Vacancy param = await GetVacancy(url);
            loadVacancies++;
            foreach (var spec in param.Specializations)
            {
                string prof = spec.ProfareaName + ": " + spec.Name;
                int id = professions.FindIndex(x => x.name == prof);
                if (id == -1)
                {
                    CollectParam x = new CollectParam
                    {
                        count = 1,
                        name = prof
                    };
                    professions.Add(x);
                }
                else
                    professions[id].count++;
            }
            foreach (var skill in param.KeySkills)
            {
                int id = skills.FindIndex(x => x.name == skill.Name);
                if (id == -1)
                {
                    CollectParam x = new CollectParam
                    {
                        count = 1,
                        name = skill.Name
                    };
                    skills.Add(x);
                }
                else
                    skills[id].count++;
            }
        }
        private async Task<Data> GetData(string url)
        {
            HttpResponseMessage a = await client.GetAsync(url);
            a.EnsureSuccessStatusCode();
            var b = await a.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Data>(b);
        }
        private async Task<Vacancy> GetVacancy(string url)
        {
            HttpResponseMessage a = await client.GetAsync(url);
            a.EnsureSuccessStatusCode();
            var b = await a.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Vacancy>(b);
        }

        private void SortLists()
        {
            CollectParam x;
            for (int i = 0; i < professions.Count; i++)
            {
                for (int j = i; j < professions.Count; j++)
                {
                    if (professions[i].count < professions[j].count)
                    {
                        x = professions[i];
                        professions[i] = professions[j];
                        professions[j] = x;
                    }
                }
            }
            for (int i = 0; i < skills.Count; i++)
            {
                for (int j = i; j < skills.Count; j++)
                {
                    if (skills[i].count < skills[j].count)
                    {
                        x = skills[i];
                        skills[i] = skills[j];
                        skills[j] = x;
                    }
                }
            }
        }

        public void PrintResultProfession(int count)
        {
            Console.Write("Топ-"+count+" Самых востребованных профессий");
            if (!salary_from.HasValue && !salary_to.HasValue)
                Console.WriteLine(":");
            else
            {
                Console.Write(" с З/П");
                if (salary_from.HasValue)
                    Console.Write(" от " + salary_from + "руб");
                if (salary_to.HasValue)
                    Console.Write(" до " + salary_to + "руб");
                Console.WriteLine(":");
            }
            int c = count <= professions.Count ? count : professions.Count;
            for (int i = 0; i < c; i++)
                Console.WriteLine((i + 1) + ") " + professions[i].name + "  " + professions[i].count);
            Console.WriteLine("Всего найдено " + allVacancies + " вакансий.");
        }

        public void PrintResultSkills(int count)
        {
            Console.Write("Топ-" + count + " Самых востребованных навыков");
            if (!salary_from.HasValue && !salary_to.HasValue)
                Console.WriteLine(":");
            else
            {
                Console.Write(" с З/П");
                if (salary_from.HasValue)
                    Console.Write(" от " + salary_from + "руб");
                if (salary_to.HasValue)
                    Console.Write(" до " + salary_to + "руб");
                Console.WriteLine(":");
            }
            int c = count <= skills.Count ? count : skills.Count;
            for (int i = 0; i < c; i++)
                Console.WriteLine((i + 1) + ") " + skills[i].name + "  " + skills[i].count);
            Console.WriteLine("Всего найдено " + allVacancies + " вакансий.");
        }
        public void PrintStatus()
        {
            switch (status)
            {
                case 0:
                    Console.Write("Соединение с API.HH.RU");
                    break;
                case 1:
                    Console.Write("Подсчет вакансий");
                    break;
                case 2:
                    Console.Write("Сбор профессий и навыков");
                    break;
                case 3:
                    Console.Write("Сортировка");
                    break;
                case 4:
                    Console.Write("Готово");
                    break;
            }
            for (int i = Console.CursorLeft; i < Console.WindowWidth - 1; i++)
                Console.Write(" ");
        }
        public void PrintLoad()
        {
            int line = Console.WindowWidth - 7;
            Console.Write("[");
            Console.BackgroundColor = ConsoleColor.White;
            for (int i = 1; i <= line; i++)
            {
                if ((allVacancies == 0) || (i * 100 / line > loadVacancies * 100 / allVacancies))
                    Console.ResetColor();
                Console.Write(" ");
            }
            Console.ResetColor();
            Console.Write("]");
            Console.Write("    ");
            Console.CursorLeft -= 4;
            if (allVacancies == 0)
                Console.Write("NaN%");
            else
                Console.Write(loadVacancies * 100 / allVacancies + "%");
        }
        private bool ExeptVacancy(int? salary_f, int? salary_t)
        {
            if ((!salary_from.HasValue) && (!salary_to.HasValue))
                return false;
            if ((!salary_f.HasValue) && (!salary_t.HasValue))
                return false;
            if (!salary_to.HasValue)
            {
                if (!salary_t.HasValue)
                    return (salary_from <= salary_f);
                if (!salary_f.HasValue)
                    return (salary_from <= salary_t);
                return ((salary_from <= salary_f) && (salary_from <= salary_t));
            }

            if (!salary_from.HasValue)
            {
                if (!salary_t.HasValue)
                    return (salary_to >= salary_f);
                if (!salary_f.HasValue)
                    return (salary_to >= salary_t);
                return ((salary_to >= salary_f) && (salary_to >= salary_t));
            }
            return (salary_from <= salary_t) && (salary_from >= salary_f);
        }
    }
    class CollectParam
    {
        public int count = 0;
        public string name = "";
    }

}