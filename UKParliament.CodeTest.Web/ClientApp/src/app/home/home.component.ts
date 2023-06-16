import { Component, OnInit } from "@angular/core";
import { NgForm } from "@angular/forms";
import { Person } from "../../models/person-view-model";
import { PersonService } from "../services/persons.service";


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

  Person = {} as Person;
  Persons: Person[] | undefined;

  constructor(private PersonService: PersonService) { }

  ngOnInit() {
    this.getPersons();
  }

  // define if a person will be created or updated
  savePerson(form: NgForm) {
    if (this.Person.id !== undefined) {
      this.PersonService.updatePerson(this.Person).subscribe(() => {
        this.cleanForm(form);
      });
    } else {
      this.PersonService.savePerson(this.Person).subscribe(() => {
        this.cleanForm(form);
      });
    }
  }

  // call the services
  getPersons() {
    this.PersonService.getPersons().subscribe((Persons: Person[]) => {
      this.Persons = Persons;
    });
  }

  // delete person
  deletePerson(Person: Person) {
    this.PersonService.deletePerson(Person).subscribe(() => {
      this.getPersons();
    });
  }

  // copy person for edit
  editPerson(Person: Person) {
    this.Person = { ...Person };
  }

  // clear form
  cleanForm(form: NgForm) {
    this.getPersons();
    form.resetForm();
    this.Person = {} as Person;
  }

}
