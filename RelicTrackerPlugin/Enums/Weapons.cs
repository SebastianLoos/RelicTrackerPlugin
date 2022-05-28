using RelicTrackerPlugin.Enums.Attributes;

namespace RelicTrackerPlugin.Enums;

internal enum WeaponCategory
{
    [WeaponCategoryName("Zodiac Weapons (50)"), Jobs(new WeaponJob[] {WeaponJob.PLD, WeaponJob.PLD_OH, WeaponJob.WAR, WeaponJob.WHM, 
        WeaponJob.SCH, WeaponJob.MNK, WeaponJob.DRG, WeaponJob.NIN, WeaponJob.BRD, WeaponJob.BLM, WeaponJob.SMN})]ZodiacWeapons,
    [WeaponCategoryName("Anima Weapons (60)")]AnimaWeapons,
    [WeaponCategoryName("Eurekan Weapons (70)")]EurekanWeapons,
    [WeaponCategoryName("Resistance Weapons (80)")]ResistanceWeapons,
    [WeaponCategoryName("Item Overview")]ItemOverview
}

internal enum WeaponJob
{
    [WeaponJobAbbreviation("UNK")]UNKNOWN,
    PLD,
    PLD_OH,
    WAR,
    DRK,
    GNB,
    WHM,
    SCH,
    AST,
    SGE,
    MNK,
    DRG,
    NIN,
    SAM,
    RPR,
    BRD,
    MCH,
    DNC,
    BLM,
    SMN,
    RDM,
    BLU
}
