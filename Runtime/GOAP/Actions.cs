﻿using System.Collections.Generic;

namespace Kickstarter.GOAP
{
    public class AgentAction
    {
        private AgentAction(string name)
        {
            Name = name;
        }

        public string Name { get; }
        public float Cost { get; private set; }

        public HashSet<AgentBelief> Preconditions { get; } = new HashSet<AgentBelief>();
        public HashSet<AgentBelief> Effects { get; } = new HashSet<AgentBelief>();

        private IActionStrategy strategy;
        public bool Complete => strategy.Complete;

        public void Start() => strategy.Start();
        public void Update(float deltaTime)
        {
            if (strategy.CanPerform)
                strategy.Update(deltaTime);

            if (!strategy.Complete)
                return;

            foreach (var effect in Effects)
                effect.Evaluate();
        }
        public void Stop() => strategy.Stop();

        public class Builder
        {
            private readonly AgentAction action;

            public Builder(string name)
            {
                action = new AgentAction(name) 
                {
                    Cost = 1 
                };
            }

            public Builder WithCost(float cost)
            {
                action.Cost = cost;
                return this;
            }

            public Builder WithStrategy(IActionStrategy strategy)
            {
                action.strategy = strategy;
                return this;
            }

            public Builder AddPrecondition(AgentBelief belief)
            {
                action.Preconditions.Add(belief);
                return this;
            }

            public Builder AddEffect(AgentBelief belief)
            {
                action.Effects.Add(belief);
                return this;
            }

            public AgentAction Build() => action;
        }
    }
}
