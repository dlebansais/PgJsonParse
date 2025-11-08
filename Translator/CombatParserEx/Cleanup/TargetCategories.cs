namespace Translator;

internal enum TargetCategories
{
    None = 0,
    Self = 0x00000001,
    Ally = 0x00000002,
    Pet = 0x00000004,
    TargetPet = 0x00000008,
    Ennemy = 0x00000010,
    AllEnnemies = 0x00000020,
}
