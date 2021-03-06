﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VVVV.PluginInterfaces.V2;
using BulletSharp;
using VVVV.Utils.VMath;
using VVVV.Internals.Bullet;
using System.ComponentModel.Composition;
using VVVV.Core.Logging;
using VVVV.Bullet.DataTypes.Vehicle;

namespace VVVV.Nodes.Bullet
{
    [PluginInfo(Name = "WheelInfo", Category = "Bullet", Version ="Create",
        Help = "Retrieves id for a rigid body", Author = "vux")]
    public unsafe class BulleteCreateWheelInfoNode : IPluginEvaluate
    {
        [Input("Suspension Stiffness", DefaultValue =20.0f)]
        protected ISpread<float> SuspensionStiffness;

        [Input("Wheels Damping Relaxation", DefaultValue =2.3f)]
        protected ISpread<float> WheelsDampingRelaxation;

        [Input("Wheels Damping Compression", DefaultValue = 4.4f)]
        protected ISpread<float> WheelsDampingCompression;

        [Input("Friction Slip", DefaultValue = 1000)]
        protected ISpread<float> FrictionSlip;

        [Input("Roll Influence", DefaultValue = 0.1f)]
        protected ISpread<float> RollInfluence;

        [Output("Output")]
        protected ISpread<WheelInfoSettings> output;

        public void Evaluate(int SpreadMax)
        {
            this.output.SliceCount = SpreadMax;

            for (int i = 0; i < SpreadMax; i++)
            {
                this.output[i] = new WheelInfoSettings()
                {
                    FrictionSlip = FrictionSlip[i],
                    RollInfluence = RollInfluence[i],
                    SuspensionStiffness = SuspensionStiffness[i],
                    WheelsDampingCompression = WheelsDampingCompression[i],
                    WheelsDampingRelaxation = WheelsDampingRelaxation[i]
                };
            }
        }
    }
}
