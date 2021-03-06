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

namespace VVVV.Nodes.Bullet
{
	[PluginInfo(Name="GetBodyId",Category="Bullet",
		Help = "Retrieves id for a rigid body", Author = "vux")]
	public unsafe class BulletGetBodyIdTransformNode : IPluginEvaluate
	{
		[Input("Bodies")]
        protected Pin<RigidBody> bodies;

		[Output("Id")]
        protected ISpread<int> id;

		public void Evaluate(int SpreadMax)
		{
			if (this.bodies.PluginIO.IsConnected)
			{
                this.id.SliceCount = this.bodies.SliceCount;

				for (int i = 0; i < SpreadMax; i++)
				{
					RigidBody body = this.bodies[i];
                    BodyCustomData bd = (BodyCustomData)body.UserObject;
                    this.id[i] = bd.Id;
                }
			}
			else
			{
				this.id.SliceCount = 0;
			}

		}
	}
}
