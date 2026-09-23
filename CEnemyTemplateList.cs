using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnemyEditor
{
    public class CEnemyTemplateList
    {
        private List<CEnemyTemplate> enemies;

        public CEnemyTemplateList()
        {
            enemies = new List<CEnemyTemplate>();
        }

        public void AddEnemy(string name, string iconName, int baseLife,
            double lifeModifier, int baseGold,
            double goldModifier, double spawnChance)
        {
            CEnemyTemplate enemy = new CEnemyTemplate(name, iconName, baseLife,
                lifeModifier, baseGold, goldModifier, spawnChance);
            enemies.Add(enemy);
        }

        public CEnemyTemplate GetEnemyByName(string name)
        {
            foreach (CEnemyTemplate enemy in enemies)
            {
                if (enemy.Name == name)
                    return enemy;
            }
            return null;
        }

        public CEnemyTemplate GetEnemyByIndex(int id)
        {
            if (id < 0 || id >= enemies.Count)
                return null;
            return enemies[id];
        }

        public void DeleteEnemyByName(string name)
        {
            CEnemyTemplate enemy = GetEnemyByName(name);
            if (enemy != null)
                enemies.Remove(enemy);
        }

        public void DeleteEnemyByIndex(int id)
        {
            if (id >= 0 && id < enemies.Count)
                enemies.RemoveAt(id);
        }

        public List<string> GetListOfEnemyNames()
        {
            List<string> names = new List<string>();
            foreach (CEnemyTemplate enemy in enemies)
            {
                names.Add(enemy.Name);
            }
            return names;
        }
    }
}