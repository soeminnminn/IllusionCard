using System;

public class GameSystem
{
    public Language language { get; private set; } = Language.Japanese;

    public string UserUUID { get; private set; } = string.Empty;

    public static GameSystem Instance
    {
        get => new GameSystem();
    }

    public enum Language
    {
        Japanese,
        English,
        SimplifiedChinese,
        TraditionalChinese,
    }

    #region Wrapper
    public SexyBeachPR.CharSave SexyBeachPR_Instance(byte sex)
    {
        if (sex == 0)
        {
            var male = new SexyBeachPR.CharMale();
            male.Initialize(SexyBeachPR.CharaListInfo.Instance, sex, 0, 0);
            return male;
        }
        else
        {
            var female = new SexyBeachPR.CharFemale();
            female.Initialize(SexyBeachPR.CharaListInfo.Instance, sex, 0, 0);
            return female;
        }
    }

    public CharacterPH.CustomParameter CharacterPH_Instance(CharacterPH.SEX sex) => new CharacterPH.CustomParameter(sex);

    public StudioPH.SceneInfo StudioPH_Instance() => new StudioPH.SceneInfo();

    public CharacterKK.ChaFile CharacterKK_Instance() => new CharacterKK.ChaFile();

    public StudioKK.SceneInfo StudioKK_Instance() => new StudioKK.SceneInfo();

    public CharacterHS.CharFile CharacterHS_Instance(CharacterPH.SEX sex)
    {
        if (sex == CharacterPH.SEX.FEMALE)
            return new CharacterHS.CharFemaleFile();
        else
            return new CharacterHS.CharMaleFile();
    }

    public AIChara.ChaFile AIChara_Instance() => new AIChara.ChaFile();

    public StudioNeo.SceneInfo StudioNeo_Instance() => new StudioNeo.SceneInfo();

    public StudioNeoV2.SceneInfo StudioNeoV2_Instance() => new StudioNeoV2.SceneInfo();
    #endregion
}
