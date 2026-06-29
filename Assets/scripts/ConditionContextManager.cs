using UnityEngine;

public class ConditionContextManager : MonoBehaviour
{
    public static ConditionContextManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Get comprehensive context for a specific eye condition
    /// </summary>
    public string GetConditionContext(string condition)
    {
        switch (condition.ToLower())
        {
            case "normal":
            case "normalvision":
                return GetNormalVisionContext();
            
            case "myopia":
            case "nearsightedness":
                return GetMyopiaContext();
            
            case "hyperopia":
            case "farsightedness":
                return GetHyperopiaContext();
            
            case "presbyopia":
                return GetPresbyopiaContext();
            
            case "colorblind":
            case "colorblindness":
            case "colorgraysc ale":
            case "colorredgreen":
                return GetColorBlindnessContext();
            
            default:
                return "General eye health information. Answer any questions about vision and eye disorders.";
        }
    }

    private string GetNormalVisionContext()
    {
        return @"NORMAL VISION (Emmetropia):
Normal vision is the condition where a person can see objects clearly at all distances without needing corrective lenses. 

KEY FACTS:
- Light enters the eye and is refracted by the cornea (70% of focusing power) and lens (30%)
- Light focuses precisely on the retina
- The retina converts light to electrical signals sent to the brain via the optic nerve
- Normal visual acuity is 6/6 (metric) or 20/20 (feet)
- Accommodation: the lens changes shape to focus on near and distant objects

EYE PARTS INVOLVED:
- Cornea: transparent outer layer, provides maximum refraction
- Lens: changes shape for accommodation
- Retina: contains rods (night vision, 120 million) and cones (color vision, 6 million)
- Fovea: area of sharpest vision in retina
- Optic Nerve: carries signals to brain (1+ million fibers)
- Visual Cortex: processes visual information in occipital lobe

CHARACTERISTICS:
- Clear vision at all distances
- Good depth perception (binocular vision advantage)
- Accurate color vision (red, green, blue cones)
- Proper contrast sensitivity
- Peripheral vision ~180° horizontally, ~135° vertically
- Pupillary reflex: pupil constricts in bright light, dilates in dim light
- Dark adaptation takes 20-30 minutes

SOURCES: Mayo Clinic, WebMD, National Eye Institute (NEI)";
    }

    private string GetMyopiaContext()
    {
        return @"MYOPIA (Nearsightedness):
Myopia is a refractive error where the eye focuses light in front of the retina, causing distant objects to appear blurry while near objects are clear.

WHAT HAPPENS:
- Light rays focus in front of the retina instead of on it
- Causes: eye is too long OR cornea is too curved
- Results in blurred distance vision but normal near vision

CAUSES:
- Genetic factors (hereditary)
- Environmental factors (excessive near work, reduced outdoor time)
- Eye growth during childhood
- High myopia can be associated with myopic macular degeneration

SYMPTOMS:
- Blurred vision at distance
- Squinting to see far objects
- Eye strain
- Difficulty seeing classroom board or road signs
- Night driving problems

DIAGNOSIS:
- Visual acuity test (Snellen chart)
- Refraction test
- Ophthalmologist examination

TREATMENTS:
- Corrective lenses (glasses or contact lenses)
- LASIK or PRK eye surgery (for adults)
- Orthokeratology (overnight contact lenses)
- Myopia control in children: atropine drops, multifocal lenses, increased outdoor time

PREVENTION/MANAGEMENT:
- Increase outdoor time (especially in children)
- Reduce continuous near work
- Follow 20-20-20 rule: every 20 minutes, look at something 20 feet away for 20 seconds
- Proper lighting while reading
- Regular eye examinations

SOURCES: Mayo Clinic, WebMD, National Eye Institute, American Academy of Ophthalmology (AAO), International Myopia Institute";
    }

    private string GetHyperopiaContext()
    {
        return @"HYPEROPIA (Farsightedness):
Hyperopia is a refractive error where the eye focuses light behind the retina, causing near objects to appear blurry while distant objects are clear.

WHAT HAPPENS:
- Light rays focus behind the retina instead of on it
- Causes: eye is too short OR cornea is too flat
- Results in blurred near vision but good distance vision
- Accommodation (lens thickening) can temporarily compensate in mild cases

CAUSES:
- Genetic factors (inherited condition)
- Shorter than normal eyeball length
- Flatter than normal cornea
- Often present from birth

SYMPTOMS:
- Blurred vision at near distances
- Difficulty reading (especially in dim light)
- Eye strain after reading or close work
- Eye fatigue
- Headaches (from eye strain)
- Squinting when trying to focus closely
- In children: may cause esotropia (eye turning inward)

DIAGNOSIS:
- Visual acuity test
- Refraction test
- Retinoscopy
- Comprehensive eye exam

TREATMENTS:
- Corrective lenses: glasses or contact lenses (convex lenses)
- LASIK or PRK surgery (for adults, reshapes cornea)
- Refractive Lens Exchange (RLE) - replacing natural lens with artificial one
- Conductive Keratoplasty (CK) - radiofrequency energy reshapes cornea

PREVENTION/MANAGEMENT:
- Regular eye examinations
- Proper lighting for reading
- Take frequent breaks from close work (20-20-20 rule)
- Maintain healthy lifestyle
- Early detection in children to prevent amblyopia (lazy eye)

SOURCES: Mayo Clinic, WebMD, American Academy of Ophthalmology (AAO), National Eye Institute";
    }

    private string GetPresbyopiaContext()
    {
        return @"PRESBYOPIA (Age-related Near Vision Loss):
Presbyopia is a progressive age-related condition where the eye loses its ability to focus on near objects due to hardening of the lens.

WHAT HAPPENS:
- Lens becomes less flexible with age
- Ciliary muscles weaken
- Lens cannot change shape adequately for near vision
- Accommodation ability decreases progressively
- Typically starts around age 40-45

CAUSES:
- Natural aging of the lens (loses elasticity)
- Changes in ciliary muscles
- Age-related protein changes in lens
- Completely normal aging process (not a disease)

SYMPTOMS:
- Blurred vision at near distances
- Difficulty reading small print
- Need to hold reading material at arm's length
- Eye strain when reading
- Headaches after reading or close work
- Difficulty using smartphone or computer
- Problems in dim lighting

ONSET:
- Begins around age 40
- Progresses until about age 60-65
- Affects everyone eventually
- Can coexist with other refractive errors (myopia, hyperopia, astigmatism)

TREATMENTS:
- Reading glasses (simplest option)
- Bifocals or progressive lenses
- Contact lenses (monovision correction)
- LASIK (monovision surgery)
- Refractive Lens Exchange (RLE)
- Yuvezzi (first FDA-approved eye drop for presbyopia, 2023)
- Other emerging eye drops (Vuity, others)

MANAGEMENT:
- Regular eye exams
- Proper lighting for reading
- Adjust screen distance and brightness
- Take frequent breaks (20-20-20 rule)
- Maintain healthy eyes with vitamins C, E, omega-3s, zinc
- Protect eyes from UV exposure

SOURCES: Mayo Clinic, WebMD, American Academy of Ophthalmology (AAO), National Eye Institute";
    }

    private string GetColorBlindnessContext()
    {
        return @"COLOR BLINDNESS (Color Vision Deficiency):
Color blindness is a condition where the eye cannot perceive certain colors due to missing or non-functional cone cells in the retina.

TYPES:
1. RED-GREEN COLOR BLINDNESS (most common):
   - Protanopia: missing red cones (0.5% of males)
   - Deuteranopia: missing green cones (1% of males)
   - Protanomaly: red cones don't work properly (1% of males)
   - Deuteranomaly: green cones don't work properly (1% of males)
   
2. BLUE-YELLOW COLOR BLINDNESS (rare):
   - Tritanopia: missing blue cones (0.001%)
   
3. ACHROMATOPSIA (complete color blindness, extremely rare):
   - Cannot see any colors, only shades of gray

WHAT HAPPENS:
- Cone cells in the retina are missing or non-functional
- Red, green, or blue cone cells don't transmit proper signals to brain
- Brain cannot distinguish between certain colors
- Rods still work normally (night vision unaffected)

CAUSES:
- Genetic factors (X-linked recessive gene)
- More common in males (8% of males, 0.5% of females)
- Can be acquired due to eye disease or medication
- Present from birth in inherited form

SYMPTOMS:
- Difficulty distinguishing red from green (most common)
- Confusion with certain color combinations
- Seeing colors as different shades of brown, gray, or other colors
- No pain or vision loss (only color perception affected)
- Often unaware of condition until tested

DIAGNOSIS:
- Ishihara Color Test (colored dot patterns)
- Farnsworth D-15 test
- Anomaloscope (most accurate)
- Color Vision Test by ophthalmologist

IMPACT ON DAILY LIFE:
- May have difficulty with traffic lights (but learn by position)
- Challenges in selecting clothing
- May struggle with certain visual tasks (electrical wiring, design work)
- Most daily activities unaffected
- Peripheral vision, night vision, and visual acuity normal

TREATMENTS:
- Currently NO cure for inherited color blindness
- Corrective lenses: EnChroma glasses (enhance color perception)
- New research: gene therapy showing promise in animal studies
- Workplace accommodations and awareness

MANAGEMENT:
- Accept condition as normal variation
- Use color-blind friendly tools and applications
- Request accessibility accommodations at work/school
- Inform friends, family, employers of condition
- Regular eye examinations

SOURCES: Mayo Clinic, Colorblind Awareness Organization, American Academy of Ophthalmology (AAO), National Eye Institute, WebMD";
    }

    /// <summary>
    /// Get source links for a condition (for citations)
    /// </summary>
    public string GetSourceLinks(string condition)
    {
        switch (condition.ToLower())
        {
            case "normal":
            case "normalvision":
                return "Sources: Mayo Clinic, WebMD, National Eye Institute (NEI)";
            
            case "myopia":
                return "Sources: Mayo Clinic, WebMD, National Eye Institute, American Academy of Ophthalmology, International Myopia Institute";
            
            case "hyperopia":
                return "Sources: Mayo Clinic, WebMD, American Academy of Ophthalmology, National Eye Institute";
            
            case "presbyopia":
                return "Sources: Mayo Clinic, WebMD, American Academy of Ophthalmology, National Eye Institute";
            
            case "colorblind":
            case "colorblindness":
            case "colorgraysc ale":
            case "colorredgreen":
                return "Sources: Mayo Clinic, Colorblind Awareness Organization, American Academy of Ophthalmology, National Eye Institute, WebMD";
            
            default:
                return "Sources: Mayo Clinic, WebMD, National Eye Institute, American Academy of Ophthalmology";
        }
    }
}
