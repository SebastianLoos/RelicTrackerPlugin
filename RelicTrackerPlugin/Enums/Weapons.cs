using RelicTrackerPlugin.Enums.Attributes;

namespace RelicTrackerPlugin.Enums;

/// <summary>
/// Represents a category (series) of a relic weapon.
/// </summary>
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

/// <summary>
/// Represents a step of one category of relic weapons.
/// </summary>
internal enum WeaponStep
{
    [WeaponStepName("Relic"), WeaponSubSteps(new WeaponSubStep[] { WeaponSubStep.RelicWeaponsmithLegend, WeaponSubStep.RelicRelicReborn })]Relic,
    [WeaponStepName("Zenith"), WeaponSubSteps(new WeaponSubStep[] { WeaponSubStep.Zenith })]Zenith,
    [WeaponStepName("Atma"), WeaponSubSteps(new WeaponSubStep[] { WeaponSubStep.AtmaUpInArms, WeaponSubStep.AtmaAtmas })]Atma,
    [WeaponStepName("Animus")]Animus,
    [WeaponStepName("Novus")]Novus,
    [WeaponStepName("Nexus")]Nexus,
    [WeaponStepName("Zodiac")]Zodiac,
    [WeaponStepName("Zeta")]Zeta,
    [WeaponStepName("Animated")]Animated
}

internal enum WeaponSubStep
{
    [WeaponSubStepQuest(WeaponQuestSet.RelicWeaponsmithLegend)]RelicWeaponsmithLegend,
    [WeaponSubStepQuest(WeaponQuestSet.RelicRelicReborn)]RelicRelicReborn,
    [WeaponSubStepItems(new WeaponItem[] { WeaponItem.ThavnairianMist }, new int[] { 3 })]Zenith,
    [WeaponSubStepQuest(WeaponQuestSet.AtmaUpInArms)]AtmaUpInArms,
    [WeaponSubStepItems(new WeaponItem[] { WeaponItem.AtmaLion, WeaponItem.AtmaWaterBearer, WeaponItem.AtmaRam, WeaponItem.AtmaCrab, WeaponItem.AtmaFish, WeaponItem.AtmaBull, WeaponItem.AtmaScales, WeaponItem.AtmaTwins, WeaponItem.AtmaScoprion, WeaponItem.AtmaArcher, WeaponItem.AtmaGoat, WeaponItem.AtmaMaiden }, new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 })]AtmaAtmas
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

internal enum WeaponQuestSet
{
    Unknown,
    [WeaponQuestSet(new WeaponQuest[] { WeaponQuest.RelicWeaponsmithLegend }, WeaponQuestType.OneTime)] RelicWeaponsmithLegend,
    [WeaponQuestSet(new WeaponQuest[] { 0, WeaponQuest.RelicRelicRebornPLD, WeaponQuest.RelicRelicRebornPLD, WeaponQuest.RelicRelicRebornWAR, 0, 0, WeaponQuest.RelicRelicRebornWHM, WeaponQuest.RelicRelicRebornSCH, 0, 0, WeaponQuest.RelicRelicRebornMNK, WeaponQuest.RelicRelicRebornDRG, WeaponQuest.RelicRelicRebornNIN, 0, 0, WeaponQuest.RelicRelicRebornBRD, 0, 0, WeaponQuest.RelicRelicRebornBLM, WeaponQuest.RelicRelicRebornSMN, 0, 0 }, WeaponQuestType.JobSpecific)] RelicRelicReborn,
    [WeaponQuestSet(new WeaponQuest[] { WeaponQuest.AtmaUpInArms }, WeaponQuestType.OneTime)] AtmaUpInArms
}

internal enum WeaponQuest
{
    Unknown,
    [WeaponQuest(66241, new WeaponQuestStep[] { })] RelicWeaponsmithLegend,
    [WeaponQuest(66656, new WeaponQuestStep[] { WeaponQuestStep.RelicRelicRebornPLDBrokenWeapon, WeaponQuestStep.RelicRelicRebornPLDClassWeapon, WeaponQuestStep.RelicRelicRebornPLDOil })] RelicRelicRebornPLD,
    [WeaponQuest(66655, new WeaponQuestStep[] { WeaponQuestStep.RelicRelicRebornWARClassWeapon })] RelicRelicRebornWAR,
    [WeaponQuest(66660, new WeaponQuestStep[] { WeaponQuestStep.RelicRelicRebornWHMClassWeapon })] RelicRelicRebornWHM,
    [WeaponQuest(66663, new WeaponQuestStep[] { WeaponQuestStep.RelicRelicRebornSCHClassWeapon })] RelicRelicRebornSCH,
    [WeaponQuest(66657, new WeaponQuestStep[] { WeaponQuestStep.RelicRelicRebornMNKClassWeapon })] RelicRelicRebornMNK,
    [WeaponQuest(66658, new WeaponQuestStep[] { WeaponQuestStep.RelicRelicRebornDRGClassWeapon })] RelicRelicRebornDRG,
    [WeaponQuest(67115, new WeaponQuestStep[] { WeaponQuestStep.RelicRelicRebornNINClassWeapon })] RelicRelicRebornNIN,
    [WeaponQuest(66661, new WeaponQuestStep[] { WeaponQuestStep.RelicRelicRebornBRDClassWeapon })] RelicRelicRebornBRD,
    [WeaponQuest(66659, new WeaponQuestStep[] { WeaponQuestStep.RelicRelicRebornBLMClassWeapon })] RelicRelicRebornBLM,
    [WeaponQuest(66662, new WeaponQuestStep[] { WeaponQuestStep.RelicRelicRebornSMNClassWeapon })] RelicRelicRebornSMN,
    [WeaponQuest(66971, new WeaponQuestStep[] { })] AtmaUpInArms
}

internal enum WeaponQuestStepType
{
    Unknown = 0,
    NPC = 1,
    Item = 2,
    Location = 3
}

internal enum WeaponQuestStep
{
    Unknown,
    [WeaponQuestLocation(23, 30, 19)] RelicRelicRebornPLDBrokenWeapon,
    [WeaponQuestItems(new WeaponItem[] { WeaponItem.AeolianScimitar, WeaponItem.Battledance3 }, new int[] { 1, 2 })] RelicRelicRebornPLDClassWeapon,
    [WeaponQuestItems(new WeaponItem[] { WeaponItem.BarbariansBardiche, WeaponItem.Battledance3 }, new int[] { 1, 2 })] RelicRelicRebornWARClassWeapon,
    [WeaponQuestItems(new WeaponItem[] { WeaponItem.ChampionsLance, WeaponItem.SavageAim3 }, new int[] { 1, 2 })] RelicRelicRebornDRGClassWeapon,
    [WeaponQuestItems(new WeaponItem[] { WeaponItem.WildlingsCesti, WeaponItem.SavageAim3 }, new int[] { 1, 2 })] RelicRelicRebornMNKClassWeapon,
    [WeaponQuestItems(new WeaponItem[] { WeaponItem.VampersKnives, WeaponItem.HeavensEye3 }, new int[] { 1, 2 })] RelicRelicRebornNINClassWeapon,
    [WeaponQuestItems(new WeaponItem[] { WeaponItem.LongarmsCompositeBow, WeaponItem.HeavensEye3 }, new int[] { 1, 2 })] RelicRelicRebornBRDClassWeapon,
    [WeaponQuestItems(new WeaponItem[] { WeaponItem.SanguineScepter, WeaponItem.SavageMight3 }, new int[] { 1, 2 })] RelicRelicRebornBLMClassWeapon,
    [WeaponQuestItems(new WeaponItem[] { WeaponItem.EruditesPicatrixOfCasting, WeaponItem.SavageMight3 }, new int[] { 1, 2 })] RelicRelicRebornSMNClassWeapon,
    [WeaponQuestItems(new WeaponItem[] { WeaponItem.MadmansWhisperingRod, WeaponItem.Quicktounge3 }, new int[] { 1, 2 })] RelicRelicRebornWHMClassWeapon,
    [WeaponQuestItems(new WeaponItem[] { WeaponItem.EruditesPicatrixOfHealing, WeaponItem.Quicktounge3 }, new int[] { 1, 2 })] RelicRelicRebornSCHClassWeapon,
    [WeaponQuestItems(new WeaponItem[] { WeaponItem.RadzAtHanQuenchingOil }, new int[] { 1 })] RelicRelicRebornPLDOil
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
