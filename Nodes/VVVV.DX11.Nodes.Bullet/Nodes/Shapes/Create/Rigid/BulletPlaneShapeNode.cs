﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using VVVV.PluginInterfaces.V2;

using VVVV.Utils.VMath;
using VVVV.DataTypes.Bullet;
using SlimDX;
using VVVV.Bullet.DataTypes;

namespace VVVV.Nodes.Bullet
{
	[PluginInfo(Name="Plane",Category="Bullet",Author="vux")]
	public class BulletPlaneShapeNode : IPluginEvaluate
	{
		[Input("Normal", DefaultValues = new double[] { 0, 1, 0 })]
        protected IDiffSpread<Vector3> normal;

        [Input("W")]
        protected IDiffSpread<float> w;

        [Input("Custom")]
        protected IDiffSpread<string> FCustom;

        [Input("Custom Object")]
        protected IDiffSpread<ICloneable> FCustomObj;

        [Output("Shape")]
        protected ISpread<AbstractRigidShapeDefinition> FShapes;

        public void Evaluate(int SpreadMax)
		{
			if (SpreadUtils.AnyChanged(this.w, this.normal, this.FCustom, this.FCustomObj))
			{
				this.FShapes.SliceCount = SpreadMax;

                for (int i = 0; i < SpreadMax; i++)
                {
                    PlaneShapeDefinition plane = new PlaneShapeDefinition(new BulletSharp.Vector3(this.normal[i].X, this.normal[i].Y, this.normal[i].Z), this.w[i]);
                    plane.Mass = 0.0f;
                    plane.Pose = RigidBodyPose.Default;
                    plane.CustomObject = this.FCustomObj[i];
                    plane.CustomString = this.FCustom[i];

					this.FShapes[i] = plane;
				}
			}			
		}
	}
}
