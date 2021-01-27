import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IChapter } from '../shared/Models/IPicture';

@Injectable({
  providedIn: 'root'
})
export class ChapterService {

  baseurl = 'https://localhost:5001/api/';

  private _http:HttpClient;
  constructor(http:HttpClient) { 
    this._http = http;
  }

  public GetChapterBy(chapterId:string):Observable<IChapter>
  {
    let url = this.baseurl +"chapter/"+chapterId;
    return this._http.get<IChapter>(url);
  }
}
