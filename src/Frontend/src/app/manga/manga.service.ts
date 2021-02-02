import { HttpClient } from '@angular/common/http';
import { Injectable} from '@angular/core';
import { Observable} from 'rxjs';
import { IManga } from '../shared/Models/IManga';

@Injectable({
  providedIn: 'root'
})
export class MangaService{

  private baseurl = 'https://localhost:5001/api/';
  constructor(private readonly http:HttpClient) { }
  
  getMangaById(mangaId:string):Observable<IManga>{
    let url = this.baseurl + "manga/" + mangaId;
    return this.http.get<IManga>(url);
  }
  getRandomManga():Observable<IManga>{
    let url = this.baseurl + "manga/random";
    return this.http.get<IManga>(url);
  }
}
