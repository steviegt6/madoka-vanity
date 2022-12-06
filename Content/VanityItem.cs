using System;
using Terraria;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace MadokaVanity.Content;

public sealed class VanityItem : ModItem
{
    public override string Name { get; }

    public override string Texture { get; }

    protected override bool CloneNewInstances => true;

    private Action<Item> setDefaults;

    public VanityItem(string name, string texture, Action<Item> setDefaults) {
        Name             = name;
        Texture          = texture;
        this.setDefaults = setDefaults;
    }

    public override ModItem Clone(Item newEntity) {
        var clone = (VanityItem) base.Clone(newEntity);
        clone.setDefaults = setDefaults;
        return clone;
    }

    public override void SetStaticDefaults() {
        base.SetStaticDefaults();

        CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
    }

    public override void SetDefaults() {
        base.SetDefaults();

        Item.vanity = true;
        setDefaults(Item);
    }
}
