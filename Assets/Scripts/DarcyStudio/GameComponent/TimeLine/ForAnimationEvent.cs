/***
 * Created by Darcy
 * Date: 2021年8月20日 星期五
 * Time: 下午4:13:57
 * Description: Description
 ***/

using UnityEngine;

namespace DarcyStudio.GameComponent.TimeLine
{
    public class ForAnimationEvent:MonoBehaviour
    {
        private void PlayAdmissionEffect (string eventName)
        {
        }

        /// <summary>
        /// 通用事件 主要用来处理非循环动作的finish
        /// </summary>
        /// <param name="eventName"></param>
        public void OnAnimationEvent (string eventName)
        {
        }

        /// <summary>
        /// 普攻或技能的打击点
        /// </summary>
        /// <param name="animationEvent"></param>
        private void hit_point (AnimationEvent animationEvent)
        {
        }

        /// <summary>
        /// 受击特效
        /// </summary>
        /// <param name="index"></param>
        private void hit_effect_point (int index)
        {
        }

        /// <summary>
        /// 技能附带的特效
        /// </summary>
        /// <param name="effectName"></param>
        private void effect_point (string effectName)
        {
        }

        /// <summary>
        /// 震屏事件
        /// </summary>
        /// <param name="param"></param>
        private void shake (string param)
        {
        }

        //【note!!!:只是通过这个事件用来获取移动开始时间 但是Unity animation事件必须要有监听函数 不然会有烦人的报错】
        private void move_start ()
        {
            //只用来获取移动开始时间
        }


        private void effect_point_heal ()
        {
        }

        /// <summary>
        /// 普攻移动开始
        /// </summary>
        private void attack_move_start ()
        {
        }

        /// <summary>
        /// 普攻移动结束
        /// </summary>
        private void attack_move_end ()
        {
        }

        /// <summary>
        /// 回跳移动开始
        /// </summary>
        private void jump_move_start ()
        {
        }

        /// <summary>
        /// 回跳移动结束
        /// </summary>
        private void jump_move_end ()
        {
        }

        /// <summary>
        /// 整体速度事件监听
        /// </summary>
        /// <param name="param"></param>
        private void timescale (string param)
        {
        }

        /// <summary>
        /// 向动作发起者推镜
        /// </summary>
        /// <param name="param"></param>
        private void push_camera (string param)
        {
        }

        /// <summary>
        /// 向动作发起者的目标方推镜
        /// </summary>
        /// <param name="param"></param>
        private void push_camera_to_target (string param)
        {
        }

        /// <summary>
        /// 枪的开火及隐藏事件
        /// </summary>
        /// <param name="param"></param>
        private void gun_fire (string param)
        {
        }

        /// <summary>
        /// 场景坐标下的技能特效
        /// </summary>
        /// <param name="param"></param>
        private void effect_point_range (string param)
        {
        }

        /// <summary>
        /// 向目标方发射飞行体的技能特效 [飞行体直线运动]
        /// </summary>
        /// <param name="animationEvent"></param>
        private void effect_point_bullet (AnimationEvent animationEvent)
        {
        }

        /// <summary>
        /// 向目标方发射飞行体的技能特效 [飞行体曲线运动]
        /// </summary>
        /// <param name="animationEvent"></param>
        private void effect_point_curve (AnimationEvent animationEvent)
        {
        }


        /// <summary>
        /// 特殊事件【根据目标方集合计算特效目标位置 有前排则在前排中间播放 否则在后排中间播放】
        /// </summary>
        /// <param name="animationEvent"></param>
        private void effect_point_line (AnimationEvent animationEvent)
        {
        }

        /// <summary>
        /// 类似effect_point_bullet事件，特效从英雄身上出发，飞到特定场景坐标后消失
        /// </summary>
        /// <param name="animationEvent"></param>
        private void effect_point_to_target (AnimationEvent animationEvent)
        {
        }

        /// <summary>
        /// 类似effect_point_curve事件，特效从英雄身上出发，飞到特定场景坐标后消失
        /// </summary>
        /// <param name="animationEvent"></param>
        private void effect_point_curve_to_target (AnimationEvent animationEvent)
        {
        }

        /// <summary>
        /// 掉落奖励的触发时间点
        /// </summary>
        private void drop_reward_point ()
        {
        }
    }
}