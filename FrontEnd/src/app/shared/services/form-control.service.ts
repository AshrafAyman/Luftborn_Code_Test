import { Injectable } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Injectable({
  providedIn: 'root'
})
export class FormControlService {

  constructor() { }

  listOfValidations: { [key: string]: string } = {
    'required': 'Field can\'t be empty',
  };

  isNotValid(formGroup: FormGroup, propertyNamePath: Array<string>): boolean {
    var touched = (formGroup.get(propertyNamePath)?.touched || formGroup.get(propertyNamePath)?.dirty) ?? false;
    var hasValidationError = (formGroup.get(propertyNamePath)?.invalid) ?? false;
    var notValid = touched && hasValidationError;
    return notValid;
  }

  getErrors(formGroup: FormGroup, propertyNamePath: Array<string>): Array<string> {
    var listOfErrors = new Array<string>();
    var validationErrors = formGroup.get(propertyNamePath)?.errors;
    for (const key in validationErrors) {
      var validationValue = this.listOfValidations[key];
      if (validationValue) {
        listOfErrors = [...listOfErrors, validationValue];
      }
    }
    return listOfErrors;
  }
}
