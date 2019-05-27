using System;
using System.Collections.Generic;
using System.Text;

namespace Digizuite_Challenge
{
    class Job
    {
        private int id;
        private string name;
        private string dependencies;
        private bool executed = false;
        private int group;

        public Job(int id, string name, string dependencies)
        {
            this.id = id;
            this.name = name;
            this.dependencies = dependencies;
        }

        public int getId()
        {
            return this.id;
        }

        public string getName()
        {
            return this.name;
        }

        public int[] getDependencies()
        {
            string[] dependencyArr = dependencies.Split(", ");
            int[] dependencyInts = new int[dependencyArr.Length];
            for (int i = 0; i < dependencyArr.Length; i++)
            {
                if(dependencyArr[i].Trim() == "")
                {
                    dependencyInts[i] = 0;
                }
                else
                {
                    dependencyInts[i] = Int32.Parse(dependencyArr[i]);
                }
            }
            return dependencyInts;
        }

        public void executeJob()
        {
            executed = true;
        }

        public bool hasBeenExecuted()
        {
            return this.executed;
        }

        public void setGroup(int groupNum)
        {
            this.group = groupNum;
        }

        public int getGroup()
        {
            return this.group;
        }
    }
}
