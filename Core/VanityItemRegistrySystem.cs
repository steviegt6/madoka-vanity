using System;
using JetBrains.Annotations;
using MadokaVanity.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace MadokaVanity.Core;

[UsedImplicitly]
public sealed class VanityItemRegistrySystem : ModSystem
{
    private const string brazen_blue  = "BrazenBlue";
    private const string prim_purple  = "PrimPurple";
    private const string riot_red     = "RiotRed";
    private const string yummy_yellow = "YummyYellow";

    private const string hairpin   = "Hairpin";
    private const string dress     = "Dress";
    private const string stockings = "Stockings";
    private const string headband  = "Headband";
    private const string leggings  = "Leggings";
    private const string bow       = "Bow";
    private const string hat       = "Hat";
    private const string boots     = "Boots";

    public static readonly (string, string, EquipType, Action<Item>, HairType)[] VANITY_ITEMS =
    {
        (brazen_blue,  hairpin,   EquipType.Head, basicVanity(22, 20, Item.sellPrice(silver: 15)), HairType.FullHair),
        (brazen_blue,  dress,     EquipType.Body, basicVanity(22, 28, Item.sellPrice(silver: 20)), HairType.None),
        (brazen_blue,  stockings, EquipType.Legs, basicVanity(22, 18, Item.sellPrice(silver: 20)), HairType.None),
        (prim_purple,  headband,  EquipType.Head, basicVanity(22, 12, Item.sellPrice(silver: 15)), HairType.FullHair),
        (prim_purple,  dress,     EquipType.Body, basicVanity(30, 28, Item.sellPrice(silver: 20)), HairType.None),
        (prim_purple,  leggings,  EquipType.Legs, basicVanity(22, 18, Item.sellPrice(silver: 20)), HairType.None),
        (riot_red,     bow,       EquipType.Head, basicVanity(26, 14, Item.sellPrice(silver: 15)), HairType.FullHair),
        (riot_red,     dress,     EquipType.Body, basicVanity(26, 28, Item.sellPrice(silver: 20)), HairType.None),
        (riot_red,     leggings,  EquipType.Legs, basicVanity(22, 18, Item.sellPrice(silver: 20)), HairType.None),
        (yummy_yellow, hat,       EquipType.Head, basicVanity(26, 12, Item.sellPrice(silver: 15)), HairType.HatHair),
        (yummy_yellow, dress,     EquipType.Body, basicVanity(30, 28, Item.sellPrice(silver: 20)), HairType.None),
        (yummy_yellow, boots,     EquipType.Legs, basicVanity(22, 18, Item.sellPrice(silver: 20)), HairType.None),
    };

    public override void OnModLoad() {
        base.OnModLoad();

        foreach ((string category, string subName, var equipType, var setDefaults, HairType hairType) in VANITY_ITEMS)
            addVanityItem(category, subName, equipType, setDefaults, hairType);
    }

    private void addVanityItem(string category, string subName, EquipType equipType, Action<Item> setDefaults, HairType hairType) {
        string path = $"{Mod.Name}/Assets/Vanity/{category}/{subName}";
        var    item = new VanityItem(category + subName, path, setDefaults, hairType);

        Mod.AddContent(item);
        EquipLoader.AddEquipTexture(Mod, $"{path}_{equipType.ToString()}", equipType, item);
    }

    private static Action<Item> basicVanity(int width, int height, int sellPrice) {
        return item =>
        {
            item.width  = width;
            item.height = height;
            item.value  = sellPrice;
            item.rare   = ItemRarityID.Orange;
        };
    }
}
