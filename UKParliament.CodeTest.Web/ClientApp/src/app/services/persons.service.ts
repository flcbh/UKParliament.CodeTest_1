import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';
import { Person } from '../../models/person-view-model';


@Injectable({
  providedIn: 'root'
})
export class PersonService {

  url = 'https://localhost:44252/api/person';

  // putting HttpClient
  constructor(private httpClient: HttpClient) { }

  // Headers
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  }

  // All persons
  getPersons(): Observable<Person[]> {
    console.log("List " + this.url );

    return this.httpClient.get<Person[]>(this.url)
      .pipe(
        retry(2),
        catchError(this.handleError))
  }

  // person by id
  getPersonById(id: number): Observable<Person> {
    return this.httpClient.get<Person>(this.url + '/' + id)
      .pipe(
        retry(2),
        catchError(this.handleError)
      )
  }

  // salve Person
  savePerson(person: Person): Observable<Person> {
    console.log("New " + this.url);

    return this.httpClient.post<Person>(this.url + "/create/", person, this.httpOptions)
      .pipe(
        retry(2),
        catchError(this.handleError)
      )
  }

  savePerson1(person: Person): Observable<any> {
    const headers = { 'content-type': 'application/json' }
    const body = JSON.stringify(person);
    console.log(body)
    return this.httpClient.post(this.url, body, { 'headers': headers })
  }

  // Update Person
  updatePerson(Person: Person): Observable<Person> {
    console.log("Update " + this.url);

    return this.httpClient.put<Person>(this.url + '/update/' + Person.id, JSON.stringify(Person), this.httpOptions)
      .pipe(
        retry(1),
        catchError(this.handleError)
      )
  }

  // delete Person
  deletePerson(Person: Person) {
    return this.httpClient.delete<Person>(this.url + '/delete/' + Person.id, this.httpOptions)
      .pipe(
        retry(1),
        catchError(this.handleError)
      )
  }

  // erros
  handleError(error: HttpErrorResponse) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      // Erro client
      errorMessage = error.error.message;
    } else {
      // Erro server
      errorMessage = `Error code: ${error.status}, ` + `Msn: ${error.message}`;
    }
    console.log(errorMessage);
    return throwError(errorMessage);
  };

}
