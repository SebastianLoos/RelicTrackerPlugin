using RelicTrackerPlugin.Enums.Attributes;

namespace RelicTrackerPlugin.Enums;

internal enum WeaponCategory
{
    [WeaponCategoryName("Zodiac Weapons (50)"), WeaponCategoryJobs(new WeaponJob[] {WeaponJob.PLD, WeaponJob.PLD_OH, WeaponJob.WAR, WeaponJob.WHM, 
        WeaponJob.SCH, WeaponJob.MNK, WeaponJob.DRG, WeaponJob.NIN, WeaponJob.BRD, WeaponJob.BLM, WeaponJob.SMN}), 
        WeaponCategorySteps(new WeaponStep[] { WeaponStep.Relic, WeaponStep.Zenith, WeaponStep.Atma, WeaponStep.Animus, WeaponStep.Novus, WeaponStep.Nexus, 
            WeaponStep.Zodiac, WeaponStep.Zeta})]ZodiacWeapons,
    [WeaponCategoryName("Anima Weapons (60)")]AnimaWeapons,
    [WeaponCategoryName("Eurekan Weapons (70)")]EurekanWeapons,
    [WeaponCategoryName("Resistance Weapons (80)")]ResistanceWeapons,
    [WeaponCategoryName("Item Overview")]ItemOverview
}

internal enum WeaponStep
{
    [WeaponStepName("Relic"), WeaponSubSteps(new WeaponSubStep[] { WeaponSubStep.RelicWeaponsmithLegend, WeaponSubStep.RelicRelicReborn })]Relic,
    [WeaponStepName("Zenith"), WeaponSubSteps(new WeaponSubStep[] { WeaponSubStep.Zenith })]Zenith,
    [WeaponStepName("Atma"), WeaponSubSteps(new WeaponSubStep[] { WeaponSubStep.AtmaUpInArms })]Atma,
    [WeaponStepName("Animus")]Animus,
    [WeaponStepName("Novus")]Novus,
    [WeaponStepName("Nexus")]Nexus,
    [WeaponStepName("Zodiac")]Zodiac,
    [WeaponStepName("Zeta")]Zeta,
    [WeaponStepName("Animated")]Animated
}

internal enum WeaponSubStep
{
    [WeaponSubStepQuest(WeaponQuest.RelicWeaponsmithLegend)]RelicWeaponsmithLegend,
    [WeaponSubStepQuest(WeaponQuest.RelicRelicReborn)]RelicRelicReborn,
    [WeaponSubStepItems(new WeaponItem[] { WeaponItem.ThavnairianMist }, new int[] { 3 })]Zenith,
    [WeaponSubStepQuest(WeaponQuest.AtmaUpInArms)]AtmaUpInArms
}

internal enum WeaponRelic
{
    [WeaponId(1675)]PLD,
    [WeaponId(2306)]PLD_OH,
    [WeaponId(1816)]WAR,
    [WeaponId(2052)]WHM,
    [WeaponId(2214)]SCH,
    [WeaponId(1746)]MNK,
    [WeaponId(1885)]DRG,
    [WeaponId(7888)]NIN,
    [WeaponId(1955)]BRD,
    [WeaponId(2140)]BLM,
    [WeaponId(2213)]SMN
}

internal enum WeaponQuestType
{
    OneTime,
    JobSpecific
}

internal enum WeaponQuest
{
    Unknown,
    [WeaponQuest(new uint[] { 66241 }, WeaponQuestType.OneTime)]RelicWeaponsmithLegend,
    [WeaponQuest(new uint[] { 0, 66656, 66656, 66655, 0, 0, 66660, 66663, 0, 0, 66657, 66658, 67115, 0, 0, 66661, 0, 0, 66659, 66662, 0, 0 }, WeaponQuestType.JobSpecific)]RelicRelicReborn,
    [WeaponQuest(new uint[] { 66971 }, WeaponQuestType.OneTime)] AtmaUpInArms
}

internal enum WeaponJob
{
    UNKNOWN,
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
