/***
 * Created by Darcy
 * Date: Saturday, 26 June 2021
 * Time: 15:58:41
 * Description: SelectObjectPathAttribute
 * 最近项目里想把一些动态挂载的 Prefab 通过路径加载，
 * 避免通过引用关系被提前加载
 * (其实在unity最新版本中可以处理这个问题，
 * 但是由于用的 Unity 版本较低,
 * 且升级这事也不是我能改变的，所以需要通过路径加载 prefab， 又不想通过配置表来做）
 * 所以需要将路径设置在一个 Mono 脚本上 然后通过该脚本加载 Prefab
 * 每次要手动 copy 真的烦，本想通过拖动实现，无奈重写 Inspector 修改 Mono 内的属性 通过拖拽文件可以搞定，但是二级属性就搞不定
 * 所以退而求其次 通过 PropertyDrawer 同时使用选 Object 窗口实现自动填充路径
 * 优秀的一点是这样做通用性提高了，任何字符串属性都可以使用该 Attribute, 且可以支持任何 Object 类型，有其他类型需要增加枚举就好了
 ***/

using UnityEngine;

namespace DarcyStudio.GameComponent.Attribute.SelectObjectAttribute
{
    
    public class SelectObjectPath : PropertyAttribute
    {
        public readonly SearchType SearchType;
        public readonly string     SearchFilter;

        public SelectObjectPath (SearchType a, string label = "")
        {
            SearchType = a;
            var searchLabel = string.IsNullOrEmpty (label) ? string.Empty : $"l:{label}";
            SearchFilter = $"t:{a.ToString ()} {searchLabel}";
        }
    }

    public enum SearchType
    {
        Prefab,
        AudioClip,
        Texture,
        Sprite,
        Animator,
        Font,
        Material,
        Shader,
        VideoClip
    }
}