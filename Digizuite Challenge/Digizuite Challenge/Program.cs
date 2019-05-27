using System;
using System.Collections.Generic;
using System.IO;

namespace Digizuite_Challenge
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Job> jobList = new List<Job>();
            List<Job> groupList = new List<Job>();

            //change this url to where ever you place the Jobs.csv file
            string url = "C:/Users/Tobias/Desktop/Digizuite-Challenge/Jobs.csv";

            using (StreamReader file = new StreamReader(url))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    string[] lineArr = line.Split(";");
                    jobList.Add(new Job(Int32.Parse(lineArr[0]), lineArr[1], lineArr[2]));
                }
            }

            int lowestDependency = int.MaxValue;
            
            while(groupList.Count < jobList.Count)
            {
                foreach (Job job in jobList)
                {
                    if (!groupList.Contains(job))
                    {
                        lowestDependency = job.getDependencies()[0];

                        foreach (Job otherJob in jobList)
                        {
                            if (lowestDependency < otherJob.getDependencies()[0])
                            {
                                continue;
                            }
                            else if (lowestDependency > otherJob.getDependencies()[0])
                            {
                                lowestDependency = otherJob.getDependencies()[0];
                            }
                            else
                            {
                                break;
                            }
                        }

                        if (lowestDependency == 0)
                        {
                            groupList.Add(job);
                            lowestDependency = int.MaxValue;
                        }
                        
                        foreach (Job gJob in groupList)
                        {
                            if (job.getDependencies()[0] == gJob.getId())
                            {
                                groupList.Add(job);
                                break;
                            }
                        }

                    }
                }
            }

            foreach(Job job in groupList)
            {
                if (!job.hasBeenExecuted())
                {
                    foreach (Job otherJob in groupList)
                    {
                        if (job != otherJob)
                        {
                            bool[] executedJobs = new bool[job.getDependencies().Length];
                            for (int i = 0; i < job.getDependencies().Length; i++)
                            {
                                if (job.getDependencies()[i] == otherJob.getId())
                                {
                                    if (otherJob.hasBeenExecuted())
                                    {
                                        executedJobs[i] = true;
                                    }
                                }
                            }

                            int trueCounter = 0;
                            for (int i = 0; i < executedJobs.Length; i++)
                            {
                                if (executedJobs[i] == true)
                                {
                                    trueCounter++;
                                }
                            }

                            

                            if (trueCounter == job.getDependencies().Length || (job.getDependencies()[0] == 0 && !job.hasBeenExecuted()))
                            {
                                job.executeJob();
                            }
                            
                        }
                    }                   
                }
            }

            foreach (Job job in groupList)
            {
                if (!job.hasBeenExecuted())
                {
                    int trueCount = 0;
                    foreach (Job otherJob in groupList)
                    {
                        if(job != otherJob)
                        {                            
                            for (int i = 0; i < job.getDependencies().Length; i++)
                            {
                                if (otherJob.hasBeenExecuted() == true && job.getDependencies()[i] == otherJob.getId())
                                {
                                    trueCount++;
                                }
                            }
                        }
                    }

                    if(trueCount == job.getDependencies().Length)
                    {
                        job.executeJob();
                    }
                }
            }


            foreach(Job job in groupList)
            {
                Console.WriteLine(job.getId() + " - " + job.getName());
            }

            //keeps the console open till any key is pressed
            Console.ReadKey();
        }
    }
}
