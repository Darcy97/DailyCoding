/***
 * Created by Darcy 
 * Github: https://github.com/Darcy97
 * Date: Tuesday, 29 June 2021
 * Time: 14:06:33
 * Description: AnimatorMaterial_H2_H_Dissolve_blend
 ***/

using UnityEngine;

namespace DarcyStudio.GameComponent.ForRenderer
{


    public class AnimatorMaterial_H2_H_Dissolve_blend : AnimatorMaterialBase
    {

        private const            string KeyPower = "_Power";
        private                  int    keyIDPower;
        private                  float  preValuePower;
        [SerializeField] private float  power;

        private const            string KeyAlpha = "_Alpha";
        private                  int    keyIDAlpha;
        private                  float  preValueAlpha;
        [SerializeField] private float  alpha;

        private const            string KeyRangeColorLevel = "_Range_Color_Level";
        private                  int    keyIDRangeColorLevel;
        private                  float  preValueRangeColorLevel;
        [SerializeField] private float  rangeColorLevel;

        private const                                string KeyDissAmount = "_Diss_amount";
        private                                      int    keyIDDissAmount;
        private                                      float  preValueDissAmount;
        [Range (-0.01f, 1)] [SerializeField] private float  DissAmount;

        private const                              string keyDissSoft = "_Diss_soft";
        private                                    int    keyIDDissSoft;
        private                                    float  preValueDissSoft;
        [Range (0.0f, 1)] [SerializeField] private float  dissSoft;


        protected override Material GetMaterial () => GetMaterialFromImage ();

        protected override void InitModify ()
        {
            keyIDPower           = KeyToID (KeyPower);
            keyIDAlpha           = KeyToID (KeyAlpha);
            keyIDRangeColorLevel = KeyToID (KeyRangeColorLevel);
            keyIDDissAmount      = KeyToID (KeyDissAmount);
            keyIDDissSoft        = KeyToID (keyDissSoft);

            preValuePower           = GetFloat (keyIDPower);
            preValueAlpha           = GetFloat (keyIDAlpha);
            preValueRangeColorLevel = GetFloat (keyIDRangeColorLevel);
            preValueDissAmount      = GetFloat (keyIDDissAmount);
            preValueDissSoft        = GetFloat (keyIDDissSoft);
        }

        protected override void UpdateMaterial ()
        {
            SetFloat (keyIDPower,           power,           ref preValuePower);
            SetFloat (keyIDAlpha,           alpha,           ref preValueAlpha);
            SetFloat (keyIDRangeColorLevel, rangeColorLevel, ref preValueRangeColorLevel);
            SetFloat (keyIDDissAmount,      DissAmount,      ref preValueDissAmount);
            SetFloat (keyIDDissSoft,        dissSoft,        ref preValueDissSoft);
        }

    }
}