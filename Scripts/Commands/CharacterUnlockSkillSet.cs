using System;
using System.Collections.Generic;

[Serializable]
public class CharacterUnlockSkillSet
{
    public CharacterData character;

    public List<SkillData> unlockableSkills =
        new List<SkillData>();
}