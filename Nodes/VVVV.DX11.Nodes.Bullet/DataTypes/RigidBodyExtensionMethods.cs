﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BulletSharp;
using VVVV.DataTypes.Bullet;
using VVVV.Internals.Bullet;

namespace VVVV.Bullet.DataTypes
{
    public static class RigidBodyExtensionMethods
    {
        public static void ApplyProperties(this RigidBody rigidBody,  ref RigidBodyProperties properties)
        {
            if (properties.IsActive == false)
            {
                rigidBody.ActivationState = ActivationState.DisableSimulation;
            }
            if (properties.HasContactResponse == false)
            {
                rigidBody.CollisionFlags |= CollisionFlags.NoContactResponse;
            }
            if (properties.DebugViewEnabled == false)
            {
                rigidBody.CollisionFlags = CollisionFlags.DisableVisualizeObject;
            }
        }

        public static void ApplyRigidBodyProperties(this RigidBodyConstructionInfo constructionInfo, ref RigidBodyProperties properties)
        {
            constructionInfo.Friction = properties.Friction;
            constructionInfo.Restitution = properties.Restitution;
            constructionInfo.RollingFriction = properties.RollingFriction;
        }

        public static void ApplyMotionProperties(this RigidBody rigidBody, ref RigidBodyMotionProperties motionProperties)
        {
            rigidBody.LinearVelocity = motionProperties.LinearVelocity;
            rigidBody.AngularVelocity = motionProperties.AngularVelocity;
            if (motionProperties.AllowSleep == false)
            {
                rigidBody.ActivationState = ActivationState.DisableDeactivation;
            }
        }

        public static Tuple<RigidBody, int> CreateRigidBody(this BulletRigidSoftWorld world, CollisionShape collisionShape, ref RigidBodyPose initialPose, ref RigidBodyProperties bodyProperties, ref BulletSharp.Vector3 localInertia, float mass)
        {
            DefaultMotionState motionState = new DefaultMotionState((BulletSharp.Matrix)initialPose);

            RigidBodyConstructionInfo constructionInfo = new RigidBodyConstructionInfo(mass, motionState, collisionShape, localInertia);
            constructionInfo.ApplyRigidBodyProperties(ref bodyProperties);

            RigidBody rigidBody = new RigidBody(constructionInfo);
            rigidBody.CollisionFlags = CollisionFlags.None;
            rigidBody.ApplyProperties(ref bodyProperties);

            BodyCustomData customData = new BodyCustomData();
            customData.Id = world.GetNewBodyId();

            rigidBody.UserObject = customData;

            world.Register(rigidBody);
            return new Tuple<RigidBody, int>(rigidBody, customData.Id);
        }
    }
}
