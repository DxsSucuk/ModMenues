using System;
using System.Reflection;
using Lv1;
using UnityEngine;
using UnityModManagerNet;

namespace MirrorModMenu
{
    public class Main
    {
        /*
         * This function will load the Mod into the Unity Mod Manager. 
         */
        static bool Load(UnityModManager.ModEntry modEntry)
        {
            modEntry.Logger.Log("Injected Mod Menu.");
            modEntry.OnGUI = OnGUI;
            return true;
        }
        
        /*
         * This function will initialize all gui elements for the Unity Mod Manager Menu
         */
        static void OnGUI(UnityModManager.ModEntry modEntry)
        {
            GUILayout.Label("Cheat Menu");

            /*
             * Adds 1000 to the money variable.
            */
            if (GUILayout.Button("Give 1k"))
            {
                GameTool.roleData.Money += 1000;
                GameTool.Save();
            }
            
            /*
             * Adds 1000 to the player's health variable.
            */
            if (GUILayout.Button("Heal 1k"))
            {
                try
                {
                    StarBox.Instance.Player.CurHP += 1000;
                }
                catch (Exception ex)
                {
                    modEntry.Logger.LogException(ex);
                }
            }
            
            /*
             * boobas good, boobash best!
             * boobas hake
            */
            if (GUILayout.Button("Break enemy clothes"))
            {
                try
                {
                    var strArray1 = StarBox.Instance.Enemy.TemplateData.BrokeClothRule.Split(',');
                    var num1 = strArray1.Length;
                    var num2 = int.Parse(strArray1[strArray1.Length-1].Split(':')[4]);
                    new Message(ConstantData.EVENT_TRIGGER_BROKE_CLOTH, StarBox.Instance)
                    {
                        {
                            "level",
                            num1
                        },
                        {
                            "buffId",
                            num2
                        }
                    }.Send();
                }
                catch (Exception ex)
                {
                    modEntry.Logger.LogException(ex);
                    modEntry.Logger.Log(ex.HelpLink);
                    modEntry.Logger.Log(ex.StackTrace);
                    modEntry.Logger.Log(ex.Message);
                }
            }
            
            /*
             * Sets the enemy's health to 1 so that you can one hit it.
            */
            if (GUILayout.Button("Try to kill enemy"))
            {
                try
                {
                    StarBox.Instance.Enemy.CurHP = 1;
                }
                catch (Exception ex)
                {
                    modEntry.Logger.LogException(ex);
                }
            }
            
            /*
             * Sets the Turns variable to 0.
             * Reflection was used since the Turns var is private.
            */
            if (GUILayout.Button("Reset turns"))
            {
                try
                {
                    var t = StarBox.Instance;
                    var prop = t.GetType().GetField("<Turn>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance);
                    if (prop == null)
                    {
                        modEntry.Logger.Log("ERROR - Turn variable was not found!");
                        return;
                    }
                    prop.SetValue(t, 0);
                }
                catch (Exception exception)
                {
                    modEntry.Logger.LogException(exception);
                }
            }

            /*
             * Will unlock every achievement in the game so that you can have 100% in steam.
            */
            if (GUILayout.Button("Unlock all Achievements"))
            {
                GameTool.SetAchievement("skill_10");
                GameTool.SetAchievement("skill_15");
                GameTool.SetAchievement("prop_6");
                GameTool.SetAchievement("prop_11");
                GameTool.SetAchievement("slot_2");
                GameTool.SetAchievement("slot_4");
                GameTool.SetAchievement("hp_2");
                GameTool.SetAchievement("hp_4");
                GameTool.SetAchievement("phy_2");
                GameTool.SetAchievement("phy_4");
                GameTool.SetAchievement("magic_2");
                GameTool.SetAchievement("magic_4");
                GameTool.SetAchievement("cure_2");
                GameTool.SetAchievement("cure_4");
                GameTool.SetAchievement("rage_2");
                GameTool.SetAchievement("rage_4");
                GameTool.SetAchievement("Abyss_8");
                GameTool.SetAchievement("Abyss_12");
                GameTool.SetAchievement("Damage_1200");
                GameTool.SetAchievement("Damage_2400");
                GameTool.SetAchievement("MaxRage_100");
                GameTool.SetAchievement("MaxRage_200");
                GameTool.SetAchievement("Dispel_7");
                GameTool.SetAchievement("Dispel_12");
                GameTool.SetAchievement("Combo_7");
                GameTool.SetAchievement("Combo_11");
                GameTool.SetAchievement("SuperStar_2");
                GameTool.SetAchievement("SuperStar_4");
                GameTool.SetAchievement("LevelUp_2_6");
                GameTool.SetAchievement("LevelUp_2_12");
                GameTool.SetAchievement("LevelUp_3_6");
                GameTool.SetAchievement("LevelUp_3_12");
                
                foreach (BaseData baseData in GameTool.roleData.dataList)
                {
                    GameTool.SetAchievement("jieju_" + baseData.id + "_1");
                    GameTool.SetAchievement("jieju_" + baseData.id + "_2");
                    GameTool.SetAchievement("tiaojiao_" + baseData.id + "_1");
                    GameTool.SetAchievement("tiaojiao_" + baseData.id + "_2");
                    GameTool.SetAchievement("wancheng_" + baseData.id);
                    GameTool.SetAchievement("Kill_25_" + baseData.id);
                    GameTool.SetAchievement("poyi_" + baseData.id);
                }
                
                modEntry.Logger.Log("Finished adding Achievements! :>");
                
            }
        }
    }
}