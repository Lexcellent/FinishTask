using System;
using System.Reflection;
using Duckov.Quests;
using HarmonyLib;
using UnityEngine;

namespace FinishTask
{
    public class ModBehaviour : Duckov.Modding.ModBehaviour
    {
        private Harmony? _harmony = null;

        protected override void OnAfterSetup()
        {
            Debug.Log("FinishTask模组：OnAfterSetup方法被调用");
            if (_harmony != null)
            {
                Debug.Log("FinishTask模组：已修补 先卸载");
                _harmony.UnpatchAll();
            }

            Debug.Log("FinishTask模组：执行修补");
            _harmony = new Harmony("Lexcellent.FinishTask");
            _harmony.PatchAll(Assembly.GetExecutingAssembly());
            Debug.Log("FinishTask模组：修补完成");
        }

        protected override void OnBeforeDeactivate()
        {
            Debug.Log("FinishTask模组：OnBeforeDeactivate方法被调用");
            Debug.Log("FinishTask模组：执行取消修补");
            if (_harmony != null)
            {
                _harmony.UnpatchAll();
            }

            Debug.Log("FinishTask模组：执行取消修补完毕");
        }
    }

    [HarmonyPatch(typeof(Task))]
    public static class TaskPatch
    {
        [HarmonyPatch("IsFinished")]
        [HarmonyPrefix]
        public static bool PrefixIsFinished(Task __instance, ref bool __result)
        {
            try
            {
                __result = true;
                return false;
            }
            catch (Exception e)
            {
                Debug.Log($"FinishTask模组：错误：{e.Message}");
                return true;
            }
        }
    }
}