using Core.Commands.Support;
using MediatR;
using Microsoft.Extensions.Logging;
using FluentValidation;
using FluentValidation.Results;

namespace Core.Tests.Commands
{

    public class BaseCreateCommandTests
    {

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void HandlerCanBeConstructed()
        {
            new TestCommand.Handler(Validator, MockLogger.Object, MockMediator.Object)
                .Should()
                .NotBeNull();
        }

        [Test]
        public async Task HandlerCanHandleCommand()
        {
            // arrange 
            var hdlr = new TestCommand.Handler(Validator, MockLogger.Object, MockMediator.Object);

            // act  
            var result = await hdlr.Handle(Command, CancellationToken.None);

            // assert
            result.Should().NotBeNull();
        }

        [Test]
        public async Task HandlerCanExecutesCommand()
        {
            // arrange 
            var hdlr = new TestCommand.Handler(Validator, MockLogger.Object, MockMediator.Object);
            var cmd = Command;

            // act  
            var result = await hdlr.Handle(cmd, CancellationToken.None);

            // assert
            hdlr.HandlerCalled.Should().BeTrue();
            result.Success.Should().BeTrue();
            result.Name.Should().BeSameAs(cmd.Name);
        }

        [Test]
        public async Task HandlerCanExecutesCommandUnlessValidationFails()
        {
            // arrange 
            var hdlr = new TestCommand.Handler(FailingValidator(), MockLogger.Object, MockMediator.Object);

            // act  
            var result = await hdlr.Handle(Command, CancellationToken.None);

            // assert
            hdlr.HandlerCalled.Should().BeFalse();
            result.Success.Should().BeFalse();
            result.Errors.Should().NotBeEmpty();
        }

        [Test]
        public async Task HandlerPublishesEvent()
        {
            // arrange 
            var mediator = MockMediator;
            var hdlr = new TestCommand.Handler(Validator, MockLogger.Object, mediator.Object);
            var cmd = Command;

            // act  
            var result = await hdlr.Handle(Command, CancellationToken.None);

            // assert
            mediator.Verify(x => x.Publish(It.IsAny<CommandCompletedEvent<TestCommand.Result>>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task HandlerPublishesEventUnlessValidationFails()
        {
            // arrange 
          
            var mediator = MockMediator;
            var hdlr = new TestCommand.Handler(FailingValidator(), MockLogger.Object, mediator.Object);
            var cmd = Command;

            // act  
            var result = await hdlr.Handle(Command, CancellationToken.None);

            // assert
            mediator.Verify(x => x.Publish(It.IsAny<CommandCompletedEvent<TestCommand.Result>>(), It.IsAny<CancellationToken>()), Times.Never);
        }


        [Test]
        public async Task HandlerTimesCommand()
        {
            // arrange  
            var delayinMs = 100;
            var hdlr = new TestCommand.Handler(Validator, MockLogger.Object, MockMediator.Object);
            hdlr.DelayInMs = delayinMs;

            // act  
            var result = await hdlr.Handle(Command, CancellationToken.None);

            // assert
            result.DurationInMs.Should().BeGreaterThanOrEqualTo(delayinMs);
        }

        [Test]
        public async Task HandlerTimesCommandIfValidationFails()
        {
            // arrange       
            var hdlr = new TestCommand.Handler(FailingValidator(), MockLogger.Object, MockMediator.Object);  

            // act  
            var result = await hdlr.Handle(Command, CancellationToken.None);

            // assert
            result.DurationInMs.Should().BeGreaterThanOrEqualTo(1);
        }


        //  ----------------------------------------------------
        // --------------    Helpers                -------------
        //  ----------------------------------------------------

        
        private static TestCommand.Command Command => new(Guid.NewGuid().ToString() );
        private static Mock<ILogger<TestCommand.Handler>> MockLogger => new();
        private static Mock<IMediator> MockMediator => new();
        private static TestCommand.Validator Validator => new();
        private static TestCommand.Validator FailingValidator()
        {
            var validator = Validator;
            validator.ReturnedValidationResult = new ValidationResult(new[] { new ValidationFailure("prop", "msg") });
            return validator;
        }
    }

    public class TestCommand
    {
        public record Command(string Name) : BaseCommand<Result>;
        public class Result : BaseComandResult
        {
            public string? Name { get; set; }
        }

        public class Validator : AbstractValidator<Command>
        {
            public ValidationResult ReturnedValidationResult { get;  set; } = new ValidationResult { Errors = new List<ValidationFailure>() };
            public Command? ValidatedCommand { get; private set; }
             

            public override async Task<ValidationResult> ValidateAsync(ValidationContext<Command> context, CancellationToken cancellation = default)
            {
                ValidatedCommand = context.InstanceToValidate;
                await Task.Delay(TimeSpan.FromMilliseconds(10), cancellation);               
                return ReturnedValidationResult;
            }

        }

        public class Handler : BaseCommandHandler<Command, Result>
        {
            public Handler(IValidator<Command> validator, ILogger<Handler> logger, IMediator mediator)
                : base(validator, logger, mediator)
            { }

            public bool HandlerCalled { get; private set; }
            public int DelayInMs { get; set; }

            protected override async Task<Result> HandleCommand(Command request, Result result, CancellationToken cancellationToken)
            {
                HandlerCalled = true;
                result.Name = request.Name;
                await Task.Delay(TimeSpan.FromMilliseconds(DelayInMs), cancellationToken);
                return result;
            }
        }
    }


}
