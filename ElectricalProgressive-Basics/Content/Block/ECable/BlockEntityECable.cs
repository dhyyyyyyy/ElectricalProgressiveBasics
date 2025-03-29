using System;
using ElectricalProgressive.Utils;
using Vintagestory.API.Common;
using Vintagestory.API.Datastructures;
using Vintagestory.API.Util;

namespace ElectricalProgressive.Content.Block.ECable
{
    public class BlockEntityECable : BlockEntity {
        private Facing switches = Facing.None;

        private BEBehaviorElectricalProgressive? ElectricalProgressive => GetBehavior<BEBehaviorElectricalProgressive>();

        public Facing Connection
        {
            get => this.ElectricalProgressive?.Connection ?? Facing.None;
            set
            {
                if (this.ElectricalProgressive != null)
                {
                    this.ElectricalProgressive.Connection = value;
                }
            }
        }

        //�������� �������� �� Block � BEBehaviorElectricalProgressive
        public (EParams, int) Eparams
        {
            get => this.ElectricalProgressive?.Eparams ?? default((EParams, int));
            set => this.ElectricalProgressive!.Eparams = value;
        }

        //�������� �������� �� Block � BEBehaviorElectricalProgressive
        public EParams[] AllEparams
        {
            get => this.ElectricalProgressive?.AllEparams ?? null;
            set
            {
                if (this.ElectricalProgressive != null)
                {
                    this.ElectricalProgressive.AllEparams = value;
                }
            }
        }


        public Facing Switches {
            get => this.switches;
            set => this.ElectricalProgressive!.Interruption &= this.switches = value;
        }

        public Facing SwitchesState {
            get => ~this.ElectricalProgressive!.Interruption;
            set => this.ElectricalProgressive!.Interruption = this.switches & ~value;
        }

        public override void ToTreeAttributes(ITreeAttribute tree) {
            base.ToTreeAttributes(tree);

            tree.SetBytes("electricalprogressive:switches", SerializerUtil.Serialize(this.switches));
        }

        public override void FromTreeAttributes(ITreeAttribute tree, IWorldAccessor worldAccessForResolve) {
            base.FromTreeAttributes(tree, worldAccessForResolve);

            try {
                this.switches = SerializerUtil.Deserialize<Facing>(tree.GetBytes("electricalprogressive:switches"));
            }
            catch (Exception exception) {
                this.Api?.Logger.Error(exception.ToString());
            }
        }
    }
}
