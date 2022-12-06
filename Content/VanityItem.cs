using System;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ID;
using Terraria.ModLoader;

namespace MadokaVanity.Content;

public sealed class VanityItem : ModItem
{
    public override string Name { get; }

    public override string Texture { get; }

    protected override bool CloneNewInstances => true;

    private          Action<Item> setDefaults;
    private readonly HairType     hairType;

    public VanityItem(string name, string texture, Action<Item> setDefaults, HairType hairType) {
        Name             = name;
        Texture          = texture;
        this.setDefaults = setDefaults;
        this.hairType    = hairType;
    }

    public override ModItem Clone(Item newEntity) {
        var clone = (VanityItem) base.Clone(newEntity);
        clone.setDefaults = setDefaults;
        return clone;
    }

    public override void SetStaticDefaults() {
        base.SetStaticDefaults();

        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;

        switch (hairType) {
            case HairType.None: break;

            case HairType.FullHair:
                ArmorIDs.Head.Sets.DrawFullHair[EquipLoader.GetEquipSlot(Mod, Name, EquipType.Head)] = true;
                break;

            case HairType.HatHair:
                ArmorIDs.Head.Sets.DrawHatHair[EquipLoader.GetEquipSlot(Mod, Name, EquipType.Head)] = true;
                break;

            default: throw new ArgumentOutOfRangeException();
        }
    }

    public override void SetDefaults() {
        base.SetDefaults();

        Item.vanity = true;
        setDefaults(Item);
    }
}
