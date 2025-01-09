using AIs;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Xml.Serialization;

namespace GHTweaks.Configuration.Core
{
    public class AIParam : IPatchConfig
    {
        public AI.AIID ID { get; set; } = AI.AIID.None;

        [PropertyInfo("m_Health")]
        [DefaultValue(0)]
        public float Health { get; set; } = 0;

        [PropertyInfo("m_HealthRegeneration")]
        [DefaultValue(0)]
        public float HealthRegeneration { get; set; } = 0;

        [PropertyInfo("m_AttackRange")]
        [DefaultValue(0)]
        public float AttackRange { get; set; } = 0;

        [PropertyInfo("m_Damage")]
        [DefaultValue(0)]
        public float Damage { get; set; } = 0;

        [PropertyInfo("m_JumpAttackRange")]
        [DefaultValue(0)]
        public float JumpAttackRange { get; set; } = 0;

        [PropertyInfo("m_JumpBackRange")]
        [DefaultValue(0)]
        public float JumpBackRange { get; set; } = 0;

        [PropertyInfo("m_PoisonLevel")]
        [DefaultValue(0)]
        public float PoisonLevel { get; set; } = 0;

        [PropertyInfo("m_MinBitingDuration")]
        [DefaultValue(0)]
        public float MinBitingDuration { get; set; } = 0;

        [PropertyInfo("m_MaxBitingDuration")]
        [DefaultValue(0)]
        public float MaxBitingDuration { get; set; } = 0;

        [PropertyInfo("m_EnemySenseRange")]
        [DefaultValue(0)]
        public float EnemySenseRange { get; set; } = 0;

        [PropertyInfo("m_SightAngle")]
        [DefaultValue(0)]
        public float SightAngle { get; set; } = 0;

        [PropertyInfo("m_SightRange")]
        [DefaultValue(0)]
        public float SightRange { get; set; } = 0;

        [PropertyInfo("m_HearingSneakRange")]
        [DefaultValue(0)]
        public float HearingSneakRange { get; set; } = 0;

        [PropertyInfo("m_HearingWalkRange")]
        [DefaultValue(0)]
        public float HearingWalkRange { get; set; } = 0;

        [PropertyInfo("m_HearingRunRange")]
        [DefaultValue(0)]
        public float HearingRunRange { get; set; } = 0;

        [PropertyInfo("m_HearingSwimRange")]
        [DefaultValue(0)]
        public float HearingSwimRange { get; set; } = 0;

        [PropertyInfo("m_HearingActionRange")]
        [DefaultValue(0)]
        public float HearingActionRange { get; set; } = 0;

        [XmlIgnore]
        public bool HasAtLeastOneEnabledPatch
        {
            get
            {
                var aiParam = DefaultAIParamValues.FirstOrDefault(x => x.ID == ID);
                if (aiParam == null)
                    return false;

#if DEBUG
                return true;
#endif

                foreach (var info in GetType().GetProperties())
                {
                    if (info.GetType() != typeof(float))
                        continue;

                    var thisValue = info.GetValue(this);
                    var defaultValue = info.GetValue(aiParam);
                    if (thisValue != defaultValue)
                        return true;
                }
                return false;
            }
        }

        [XmlIgnore]
        public static readonly List<AIParam> DefaultAIParamValues = new List<AIParam>
            {
                new AIParam()
                {
                    ID = AI.AIID.Puma,
                    Health = 300,
                    AttackRange = 2.4f,
                    Damage = 8,
                    EnemySenseRange = 12,
                    HearingSneakRange = 10,
                    HearingWalkRange = 10,
                    HearingRunRange = 10,
                    HearingSwimRange = 10,
                    HearingActionRange = 10,
                },
                new AIParam()
                {
                    ID = AI.AIID.Jaguar,
                    Health = 300,
                    AttackRange = 2.4f,
                    Damage = 10,
                    JumpBackRange = 2.5f,
                    EnemySenseRange = 12,
                    HearingSneakRange = 10,
                    HearingWalkRange = 10,
                    HearingRunRange = 10,
                    HearingSwimRange = 10,
                    HearingActionRange = 10,
                },
                new AIParam()
                {
                    ID = AI.AIID.BlackPanther,
                    Health = 300,
                    AttackRange = 2.4f,
                    Damage = 33,
                    EnemySenseRange = 12,
                    HearingSneakRange = 10,
                    HearingWalkRange = 10,
                    HearingRunRange = 10,
                    HearingSwimRange = 10,
                    HearingActionRange = 10,
                },
                new AIParam()
                {
                    ID = AI.AIID.BlackCaiman,
                    Health = 200,
                    AttackRange = 2.25f,
                    Damage = 50,
                    EnemySenseRange = 12,
                    HearingSneakRange = 6,
                    HearingWalkRange = 6,
                    HearingRunRange = 6,
                    HearingSwimRange = 6,
                    HearingActionRange = 6,
                },
                new AIParam()
                {
                    ID = AI.AIID.GreenAnaconda,
                    Health = 300,
                    AttackRange = 1f,
                    Damage = 25,
                },
                new AIParam()
                {
                    ID = AI.AIID.BoaConstrictor,
                    Health = 100,
                    AttackRange = 1f,
                    Damage = 30,
                },
                new AIParam()
                {
                    ID = AI.AIID.SouthAmericanRattlesnake,
                    Health = 10,
                    AttackRange = 1.3f,
                    Damage = 5.05f,
                    PoisonLevel = 2,
                    EnemySenseRange = 3,
                    HearingSneakRange = 3,
                    HearingWalkRange = 3,
                    HearingRunRange = 3,
                    HearingSwimRange = 3,
                    HearingActionRange = 3,
                },
                new AIParam()
                {
                    ID = AI.AIID.GoliathBirdEater,
                    Health = 5,
                    AttackRange = 1,
                    Damage = 10,
                    PoisonLevel = 1,
                    EnemySenseRange = 1,
                    HearingSneakRange = 1,
                    HearingWalkRange = 1,
                    HearingRunRange = 1,
                    HearingSwimRange = 1,
                    HearingActionRange = 1,
                },
                new AIParam()
                {
                    ID = AI.AIID.BrasilianWanderingSpider,
                    Health = 5,
                    AttackRange = 0.25f,
                    Damage = 5.05f,
                    PoisonLevel = 3,
                    EnemySenseRange = 1,
                    HearingSneakRange = 2,
                    HearingWalkRange = 2,
                    HearingRunRange = 2,
                    HearingSwimRange = 2,
                    HearingActionRange = 2,
                },
                new AIParam()
                {
                    ID = AI.AIID.Scorpion,
                    Health = 5,
                    AttackRange = 1,
                    Damage = 5.05f,
                    PoisonLevel = 1,
                    EnemySenseRange = 1,
                    HearingSneakRange = 1,
                    HearingWalkRange = 1,
                    HearingRunRange = 1,
                    HearingSwimRange = 1,
                    HearingActionRange = 1,

                },
                new AIParam()
                {
                    ID = AI.AIID.Crab,
                    Health = 5,
                    EnemySenseRange = 3,
                    HearingSneakRange = 3,
                    HearingWalkRange = 3,
                    HearingRunRange = 3,
                    HearingSwimRange = 3,
                    HearingActionRange = 3,

                },
                new AIParam()
                {
                    ID = AI.AIID.Mouse,
                    Health = 5,
                    EnemySenseRange = 3,
                    HearingSneakRange = 3,
                    HearingWalkRange = 3,
                    HearingRunRange = 3,
                    HearingSwimRange = 3,
                    HearingActionRange = 3,
                },
                new AIParam()
                {
                    ID = AI.AIID.Agouti,
                    Health = 100,
                    HearingSneakRange = 7,
                    HearingWalkRange = 7,
                    HearingRunRange = 7,
                    HearingSwimRange = 7,
                    HearingActionRange = 7,
                },
                new AIParam()
                {
                    ID = AI.AIID.Peccary,
                    Health = 60,
                    EnemySenseRange = 3,
                    HearingSneakRange = 3,
                    HearingWalkRange = 7,
                    HearingRunRange = 9,
                    HearingSwimRange = 4,
                    HearingActionRange = 6
                },
                new AIParam()
                {
                    ID = AI.AIID.Capybara,
                    Health = 60,
                    EnemySenseRange = 4,
                    HearingSneakRange = 3,
                    HearingWalkRange = 8,
                    HearingRunRange = 9,
                    HearingSwimRange = 4,
                    HearingActionRange = 7
                },
                new AIParam()
                {
                    ID = AI.AIID.Tapir,
                    Health = 80,
                    EnemySenseRange = 3,
                    HearingSneakRange = 3,
                    HearingWalkRange = 7,
                    HearingRunRange = 9,
                    HearingSwimRange = 4,
                    HearingActionRange = 6
                },
                new AIParam()
                {
                    ID = AI.AIID.Armadillo,
                    Health = 160,
                    EnemySenseRange = 2,
                    HearingSneakRange = 2,
                    HearingWalkRange = 4,
                    HearingRunRange = 5,
                    HearingSwimRange = 4,
                    HearingActionRange = 4
                },
                new AIParam()
                {
                    ID = AI.AIID.ArmadilloThreeBanded,
                    Health = 120,
                    EnemySenseRange = 2,
                    HearingSneakRange = 2,
                    HearingWalkRange = 4,
                    HearingRunRange = 5,
                    HearingSwimRange = 4,
                    HearingActionRange = 4
                },
                new AIParam()
                {
                    ID = AI.AIID.PoisonDartFrog,
                    Health = 5
                },
                new AIParam()
                {
                    ID = AI.AIID.CaneToad,
                    Health = 5
                },
                new AIParam()
                {
                    ID = AI.AIID.RedFootedTortoise,
                    Health = 1000
                },
                new AIParam()
                {
                    ID = AI.AIID.GreenIguana,
                    Health = 20,
                    EnemySenseRange = 5,
                    HearingSneakRange = 11,
                    HearingWalkRange = 11,
                    HearingRunRange = 11,
                    HearingSwimRange = 11,
                    HearingActionRange = 11
                },
                new AIParam()
                {
                    ID = AI.AIID.CaimanLizard,
                    Health = 15,
                    EnemySenseRange = 2,
                    HearingSneakRange = 5,
                    HearingWalkRange = 5,
                    HearingRunRange = 5,
                    HearingSwimRange = 5,
                    HearingActionRange = 5
                },
                new AIParam()
                {
                    ID = AI.AIID.Caterpillar,
                    Health = 10
                },
                new AIParam()
                {
                    ID = AI.AIID.Piranha,
                    MinBitingDuration = 0.4f,
                    MaxBitingDuration = 0.6f,
                    HearingSneakRange = 5,
                    HearingWalkRange = 5,
                    HearingRunRange = 5,
                    HearingSwimRange = 5,
                    HearingActionRange = 5
                },
                new AIParam()
                {
                    ID = AI.AIID.PeacockBass,
                    MinBitingDuration = 0.4f,
                    MaxBitingDuration = 0.6f,
                    HearingSneakRange = 5,
                    HearingWalkRange = 5,
                    HearingRunRange = 5,
                    HearingSwimRange = 5,
                    HearingActionRange = 5
                },
                new AIParam()
                {
                    ID = AI.AIID.Arowana,
                    MinBitingDuration = 0.4f,
                    MaxBitingDuration = 0.6f,
                    HearingSneakRange = 5,
                    HearingWalkRange = 5,
                    HearingRunRange = 5,
                    HearingSwimRange = 5,
                    HearingActionRange = 5
                },
                new AIParam()
                {
                    ID = AI.AIID.AngelFish,
                    MinBitingDuration = 0.4f,
                    MaxBitingDuration = 0.6f,
                    HearingSneakRange = 5,
                    HearingWalkRange = 5,
                    HearingRunRange = 5,
                    HearingSwimRange = 5,
                    HearingActionRange = 5
                },
                new AIParam()
                {
                    ID = AI.AIID.DiscusFish,
                    MinBitingDuration = 0.4f,
                    MaxBitingDuration = 0.6f,
                    HearingSneakRange = 5,
                    HearingWalkRange = 5,
                    HearingRunRange = 5,
                    HearingSwimRange = 5,
                    HearingActionRange = 5
                },
                new AIParam()
                {
                    ID = AI.AIID.Stalker,
                    Health = 1000f,
                    AttackRange = 3,
                    Damage = 10,
                    JumpBackRange = 2.5f,
                    EnemySenseRange = 20,
                    HearingSneakRange = 30,
                    HearingWalkRange = 30,
                    HearingRunRange = 30,
                    HearingSwimRange= 30,
                    HearingActionRange= 30
                },
                new AIParam()
                {
                    ID = AI.AIID.Stingray,
                    Damage = 5.05f,
                    HearingSneakRange = 5,
                    HearingWalkRange = 5,
                    HearingRunRange = 5,
                    HearingSwimRange= 5,
                    HearingActionRange= 5
                },
                new AIParam()
                {
                    ID = AI.AIID.Hunter,
                    Health = 60,
                    AttackRange = 2,
                    Damage = 5.05f,
                    JumpAttackRange = 3,
                    JumpBackRange = 2.5f,
                    EnemySenseRange = 7,
                    SightAngle = 70,
                    SightRange = 10,
                    HearingSneakRange = 5,
                    HearingWalkRange = 6,
                    HearingRunRange = 9,
                    HearingSwimRange= 6,
                    HearingActionRange= 6
                },
                new AIParam()
                {
                    ID = AI.AIID.Spearman,
                    Health = 120,
                    AttackRange = 3,
                    Damage = 5.05f,
                    JumpBackRange = 3f,
                    EnemySenseRange = 7,
                    SightAngle = 70,
                    SightRange = 10,
                    HearingSneakRange = 5,
                    HearingWalkRange = 6,
                    HearingRunRange = 9,
                    HearingSwimRange= 6,
                    HearingActionRange= 6
                },
                new AIParam()
                {
                    ID = AI.AIID.Thug,
                    Health = 350,
                    AttackRange = 3,
                    Damage = 15f,
                    EnemySenseRange = 7,
                    SightAngle = 70,
                    SightRange = 10,
                    HearingSneakRange = 5,
                    HearingWalkRange = 6,
                    HearingRunRange = 9,
                    HearingSwimRange= 6,
                    HearingActionRange= 6
                },
                new AIParam()
                {
                    ID = AI.AIID.Savage,
                    Health = 100,
                    AttackRange = 2,
                    Damage = 5.05f,
                    JumpAttackRange = 3,
                    JumpBackRange = 2.5f,
                    EnemySenseRange = 7,
                    SightAngle = 70,
                    SightRange = 10,
                    HearingSneakRange = 5,
                    HearingWalkRange = 6,
                    HearingRunRange = 9,
                    HearingSwimRange= 6,
                    HearingActionRange= 6
                },
                new AIParam()
                {
                    ID = AI.AIID.KidRunner,
                    Health = 100,
                    AttackRange = 2,
                    Damage = 5.05f,
                    JumpAttackRange = 3,
                    JumpBackRange = 2.5f,
                    EnemySenseRange = 7,
                    SightAngle = 70,
                    SightRange = 10,
                    HearingSneakRange = 5,
                    HearingWalkRange = 6,
                    HearingRunRange = 9,
                    HearingSwimRange= 6,
                    HearingActionRange= 6
                },
                new AIParam()
                {
                    ID = AI.AIID.Kid,
                    Health = 100,
                    HealthRegeneration = 5,
                    AttackRange = 2,
                    Damage = 5.05f,
                    EnemySenseRange = 6,
                    SightAngle = 70,
                    SightRange = 10,
                    HearingSneakRange = 3,
                    HearingWalkRange = 7,
                    HearingRunRange = 9,
                    HearingSwimRange= 4,
                    HearingActionRange= 6
                },
                new AIParam()
                {
                    ID = AI.AIID.AlbinoCaiman,
                    Health = 450f,
                    AttackRange = 2.25f,
                    Damage = 50,
                    EnemySenseRange = 6,
                    HearingSneakRange = 6,
                    HearingWalkRange = 6,
                    HearingRunRange = 6,
                    HearingSwimRange= 6,
                    HearingActionRange= 6
                },
                new AIParam()
                {
                    ID = AI.AIID.Quest_BlackPanther,
                    Health = 300,
                    AttackRange = 2.4f,
                    Damage = 55,
                    JumpBackRange = 2.5f,
                    EnemySenseRange = 14,
                    HearingSneakRange = 12,
                    HearingWalkRange = 12,
                    HearingRunRange = 12,
                    HearingSwimRange= 12,
                    HearingActionRange= 12
                },
                new AIParam()
                {
                    ID = AI.AIID.Tapir_baby,
                    Health = 25,
                    EnemySenseRange = 8,
                    HearingSneakRange = 10,
                    HearingWalkRange = 10,
                    HearingRunRange = 10,
                    HearingSwimRange= 10,
                    HearingActionRange= 10
                },
                new AIParam()
                {
                    ID = AI.AIID.Jaguar_Arena,
                    Health = 300,
                    AttackRange = 2.4f,
                    Damage = 10,
                    JumpBackRange = 2.5f,
                    EnemySenseRange = 12,
                    HearingSneakRange = 10,
                    HearingWalkRange = 10,
                    HearingRunRange = 10,
                    HearingSwimRange= 10,
                    HearingActionRange= 10
                },
                new AIParam()
                {
                    ID = AI.AIID.Crab_Arena,
                    Health = 5,
                    EnemySenseRange = 3,
                    HearingSneakRange = 3,
                    HearingWalkRange = 3,
                    HearingRunRange = 3,
                    HearingSwimRange= 3,
                    HearingActionRange= 3
                },
                new AIParam()
                {
                    ID = AI.AIID.Mouse,
                    Health = 5,
                    EnemySenseRange = 3,
                    HearingSneakRange = 3,
                    HearingWalkRange = 3,
                    HearingRunRange = 3,
                    HearingSwimRange= 3,
                    HearingActionRange= 3
                },
                new AIParam()
                {
                    ID = AI.AIID.Peccary_Arena,
                    Health = 60,
                    EnemySenseRange = 3,
                    HearingSneakRange = 3,
                    HearingWalkRange = 7,
                    HearingRunRange = 9,
                    HearingSwimRange= 4,
                    HearingActionRange= 6
                },
                new AIParam()
                {
                    ID = AI.AIID.Capybara_Arena,
                    Health = 60,
                    EnemySenseRange = 4,
                    HearingSneakRange = 3,
                    HearingWalkRange = 8,
                    HearingRunRange = 9,
                    HearingSwimRange= 4,
                    HearingActionRange= 7
                },
                new AIParam()
                {
                    ID = AI.AIID.RedFootedTortoise_Arena,
                    Health = 1000
                },
                new AIParam()
                {
                    ID = AI.AIID.CaimanLizard_Arena,
                    Health = 15,
                    EnemySenseRange = 2,
                    HearingSneakRange = 5,
                    HearingWalkRange = 5,
                    HearingRunRange = 5,
                    HearingSwimRange = 5,
                    HearingActionRange = 5
                },
                new AIParam()
                {
                    ID = AI.AIID.CaneToad_Arena,
                    Health = 5
                },
                new AIParam()
                {
                    ID = AI.AIID.Puma_Arena_Farmer,
                    Health = 5,
                    AttackRange = 2.4f,
                    Damage = 8,
                    EnemySenseRange = 12,
                    HearingSneakRange = 10,
                    HearingWalkRange = 10,
                    HearingRunRange = 10,
                    HearingSwimRange = 10,
                    HearingActionRange = 10
                },
                new AIParam()
                {
                    ID = AI.AIID.Jaguar_Arena_Farmer,
                    Health = 5,
                    AttackRange = 2.4f,
                    Damage = 10,
                    JumpBackRange = 2.5f,
                    EnemySenseRange = 12,
                    HearingSneakRange = 10,
                    HearingWalkRange = 10,
                    HearingRunRange = 10,
                    HearingSwimRange = 10,
                    HearingActionRange = 10
                },
                new AIParam()
                {
                    ID = AI.AIID.Savage_Arena_Tribe,
                    Health = 100,
                    AttackRange = 2,
                    Damage = 5.05f,
                    JumpAttackRange = 3,
                    JumpBackRange = 2.5f,
                    EnemySenseRange = 7,
                    SightAngle = 70,
                    SightRange = 10,
                    HearingSneakRange = 5,
                    HearingWalkRange = 6,
                    HearingRunRange = 9,
                    HearingSwimRange = 6,
                    HearingActionRange = 6
                },
                new AIParam()
                {
                    ID = AI.AIID.Spearman_Arena_Tribe,
                    Health = 120,
                    AttackRange = 3,
                    Damage = 5.05f,
                    JumpBackRange = 3,
                    EnemySenseRange = 7,
                    SightAngle = 70,
                    SightRange = 10,
                    HearingSneakRange = 5,
                    HearingWalkRange = 6,
                    HearingRunRange = 9,
                    HearingSwimRange = 6,
                    HearingActionRange = 6
                },
                new AIParam()
                {
                    ID = AI.AIID.BlackCaiman_Arena_Fishing,
                    Health = 200,
                    AttackRange = 2.25f,
                    Damage = 50,
                    EnemySenseRange = 12,
                    HearingSneakRange = 6,
                    HearingWalkRange = 6,
                    HearingRunRange = 6,
                    HearingSwimRange = 6,
                    HearingActionRange = 6
                },
                new AIParam()
                {
                    ID = AI.AIID.GiantAnteater,
                    Health = 120,
                    AttackRange = 2,
                    Damage = 20,
                    EnemySenseRange = 4,
                    HearingSneakRange = 3,
                    HearingWalkRange = 8,
                    HearingRunRange = 9,
                    HearingSwimRange= 4,
                    HearingActionRange= 7,
                },
            };
    }
}
