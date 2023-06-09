﻿// <copyright file="BehaviorFactory.cs" company="KinsonDigital">
// Copyright (c) KinsonDigital. All rights reserved.
// </copyright>

#pragma warning disable CA1303 // Do not pass literals as localized parameters
namespace Partithyst.Behaviors;

using System;
using System.Collections.Generic;
using Services;

/// <summary>
/// Creates behaviors using behavior settings.
/// </summary>
public class BehaviorFactory : IBehaviorFactory
{
    /// <summary>
    /// Creates all of the behaviors using the given <paramref name="randomizerService"/>.
    /// </summary>
    /// <param name="settings">The list of settings used to create each behavior.</param>
    /// <param name="randomizerService">The random used to randomly generate values.</param>
    /// <returns>A list of behaviors.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the <paramref name="settings"/> parameter is null.</exception>
    public IBehavior[] CreateBehaviors(BehaviorSettings[] settings, IRandomizerService randomizerService)
    {
        if (settings is null)
        {
            throw new ArgumentNullException(nameof(settings), "The parameter must not be null.");
        }

        var behaviors = new List<IBehavior>();

        // Creates all of the behaviors using the given settings
        foreach (var setting in settings)
        {
            if (setting is EasingRandomBehaviorSettings easingRandomBehaviorSettings)
            {
                behaviors.Add(new EasingRandomBehavior(easingRandomBehaviorSettings, randomizerService));
            }
            else if (setting is ColorTransitionBehaviorSettings clrTransitionBehaviorSettings)
            {
                behaviors.Add(new ColorTransitionBehavior(clrTransitionBehaviorSettings));
            }
            else if (setting is RandomChoiceBehaviorSettings randomChoiceBehaviorSettings)
            {
                behaviors.Add(new RandomColorBehavior(randomChoiceBehaviorSettings, randomizerService));
            }
            else
            {
                throw new Exception($"Unknown behavior settings of type '{setting.GetType()}'");
            }
        }

        return behaviors.ToArray();
    }
}