using GamePrototype.Items.EconomicItems;
using GamePrototype.Items.EquipItems;
using GamePrototype.Utils;
using System.Text;

namespace GamePrototype.Units
{
    public sealed class Player : Unit
    {
        private readonly Dictionary<EquipSlot, EquipItem> _equipment = new();

        public Player(string name, uint health, uint maxHealth, uint baseDamage) : base(name, health, maxHealth, baseDamage)
        {            
        }

        public override uint GetUnitDamage()
        {
            if (_equipment.TryGetValue(EquipSlot.Weapon, out var item) && item is Weapon weapon) 
            {
                return BaseDamage + weapon.Damage;
            }
            return BaseDamage;
        }

        public override void HandleCombatComplete()
        {
            var items = Inventory.Items;
            for (int i = 0; i < items.Count; i++) 
            {
                if (items[i] is EconomicItem economicItem) 
                {
                    if (items[i] is Armour armour)
                    {
                        UseEconomicItem(economicItem, armour);
                        Inventory.TryRemove(items[i]);
                    }
                }
            }
        }

        public override void AddItemToInventory(Item item)
        {
            if (item is EquipItem equipItem)
            {
                _equipment[equipItem.Slot] = equipItem;
            }
            else
            {
                base.AddItemToInventory(item);
            }
        }

        private void UseEconomicItem(EconomicItem economicItem, Armour armour)
        {
            if (economicItem is HealthPotion healthPotion) 
            {
                Health += healthPotion.HealthRestore;
            }
            if (economicItem is Grindstone grindStone)
            {
                armour.Durability += grindStone.GrindRestore;
                Console.WriteLine($"Durability {armour.Name} has been restored by 7 unit");
            }
        }

        protected override uint CalculateAppliedDamage(uint damage)
        {
            if (_equipment.TryGetValue(EquipSlot.Armour, out var item) && item is Armour armour) 
            {
                damage -= (uint)(damage * (armour.Defence / 100f));
                CalculateArmourDurability(armour);
                
                void CalculateArmourDurability(Armour armour)
                {
                    armour.Durability -= 1;
                    Console.WriteLine($"Durability {armour.Name} decreased by 1 unit");
                }
            }
            return damage;
        }

        public override string ToString()
        {
            var builder = new StringBuilder();
            builder.AppendLine(Name);
            builder.AppendLine($"Health {Health}/{MaxHealth}");
            builder.AppendLine("Loot:");
            var items = Inventory.Items;
            for (int i = 0; i < items.Count; i++) 
            {
                builder.AppendLine($"[{items[i].Name}] : {items[i].Amount}");
            }
            return builder.ToString();
        }
    }
}
