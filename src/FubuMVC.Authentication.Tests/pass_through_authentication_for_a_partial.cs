﻿using FubuMVC.Core.Continuations;
using FubuMVC.Core.Http;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace FubuMVC.Authentication.Tests
{
	[TestFixture]
	public class pass_through_authentication_for_a_partial : InteractionContext<PassThroughAuthenticationFilter>
	{
		private FubuContinuation theContinuation;

		protected override void beforeEach()
		{
			MockFor<ICurrentChain>().Stub(x => x.IsInPartial()).Return(true);
			theContinuation = ClassUnderTest.Filter();
		}

		[Test]
		public void continues_to_the_next_behavior()
		{
			theContinuation.AssertWasContinuedToNextBehavior();
		}

		[Test]
		public void does_not_apply_to_authentication()
		{
			MockFor<IAuthenticationService>().AssertWasNotCalled(x => x.TryToApply());
		}
	}
}