﻿using ClientManagement.UnitTests.DataProviders;
using Core.Presentation.Models.DataTransferObjects;
using Core.Presentation.Models.DataTransferObjects.Base;
using Core.Presentation.Models.Interfaces.Base;
using Core.Presentation.Models.Base;
using NuGet.Frameworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Presentation.ViewComponents.Components.Base;
using Core.Presentation.ViewComponents.Components;
using Core.Presentation.Models;
using Core.Presentation.Models.Validators;


namespace ClientManagement.UnitTests
{
    [TestFixture]
    public  class FormComponentTests
    {
        [TestCaseSource(typeof(FormComponentTestsDataProvider), nameof(FormComponentTestsDataProvider.FormComponentWorks_DP))]
        public static bool FormComponentBaseWorks(
            GenericListViewModel<ClientDto> viewModel, 
            ClientDto expectedOutput,
            string expectedEmail,
            bool expectedArchived
          ) 
         
        {
            Assert.That(viewModel.ViewModelState.Single(x => x.Id == Guid.Empty).Archived, Is.False);
            var compBase = new FormComponent<ClientDto>() { };
            var vm = new ClientsViewModel();
            compBase.ViewModel.Fields = vm.NewClientFormViewModel.Fields;
            
            Assert.That(compBase.ViewModel.Fields.Any(x => x.Validator != null && x.Validator.Validators.Any( y =>  y.Name == nameof(Validators.Required))) , Is.True);
            var subject = new ClientDto();
            var requiredEmailField = vm.PrimaryContactFormViewModel.Fields
                .FirstOrDefault(x => x.Validator != null && x.Name == nameof(ClientDto.PrimaryContactEmail) && x.Validator.Validators.Any(y => y.Name == nameof(Validators.Email)));
            
            Assert.That(requiredEmailField , Is.Not.Null);
            subject.PrimaryContactEmail = "invalidemail";
            var validationResult = requiredEmailField!.Validator!.Validate(subject);

            Assert.That((validationResult?.Key ?? string.Empty) == nameof(Validators.Email), Is.True);
            Assert.That(compBase.ViewModel.ViewModelState.Any(), Is.False);

            compBase.ViewModel.ViewModelState = compBase.ViewModel.ViewModelState.Append(subject);
            compBase.ViewModel.Set(subject.Id, nameof(ClientDto.PrimaryContactEmail), expectedEmail);  
            
            Assert.That(compBase.ViewModel.Get(subject.Id, nameof(ClientDto.PrimaryContactEmail), typeof(string)) == expectedEmail, Is.True);
            Assert.That(requiredEmailField.Validator.Validate(subject), Is.Null);

            compBase.OnInputChange(subject.Id, requiredEmailField, expectedEmail);
            Assert.That(compBase.ViewModel.WasValidated, Is.True);
            var requiredField = compBase.ViewModel.Fields.FirstOrDefault(x => (x.Validator?.Validators?.Any(y => y.Name == nameof(Validators.Required)) ?? false) 
            && !x.Validator.Validators.Any(z => z.Name == nameof(Validators.Email)));
            Assert.That(requiredField.Validator.Validate(subject)?.Key == nameof(Validators.Required), Is.True);

            return subject.PrimaryContactEmail == expectedOutput.PrimaryContactEmail;
        }
    }
}
