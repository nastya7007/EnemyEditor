using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EnemyEditor
{
    public class CEnemyTemplateList
    {
        [JsonInclude]
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

        //JSON

        public void SaveToJson(string path)
        {
            string json = JsonSerializer.Serialize(enemies);
            File.WriteAllText(path, json);
        }

        public void LoadFromJson(string path)
        {
            string json = File.ReadAllText(path);
            enemies = new List<CEnemyTemplate>();

            JsonDocument doc = JsonDocument.Parse(json);

            foreach (JsonElement element in doc.RootElement.EnumerateArray())
            {
                string name = element.GetProperty("Name").GetString();
                string iconName = element.GetProperty("IconName").GetString();
                int baseLife = element.GetProperty("BaseLife").GetInt32();
                double lifeModifier = element.GetProperty("LifeModifier").GetDouble();
                int baseGold = element.GetProperty("BaseGold").GetInt32();
                double goldModifier = element.GetProperty("GoldModifier").GetDouble();
                double spawnChance = element.GetProperty("SpawnChance").GetDouble();

                CEnemyTemplate enemy = new CEnemyTemplate(name, iconName, baseLife,
                    lifeModifier, baseGold, goldModifier, spawnChance);
                enemies.Add(enemy);
            }
        }
    }
}