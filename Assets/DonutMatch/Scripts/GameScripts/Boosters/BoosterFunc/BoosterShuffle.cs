using UnityEngine;

namespace DonutMatach
{
    public class BoosterShuffle : BoosterFunc
    {
        #region override
        public override bool ActivateApply()
        {
            MBoard.MixGrid(null);
          //  MSound.PlayClip(0.2f, b.prefab.privateClip);
            return true;
        }
        #endregion override
    }
}

