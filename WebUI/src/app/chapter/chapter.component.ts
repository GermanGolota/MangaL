import { Component, OnInit, Input } from '@angular/core';
import { IChapter } from '../shared/Models/IChapter';
import { ChapterService } from './chapter.service';

@Component({
  selector: 'app-chapter',
  templateUrl: './chapter.component.html',
  styleUrls: ['./chapter.component.scss']
})
export class ChapterComponent implements OnInit {

  _service:ChapterService;
  chapter:IChapter;

  @Input() chapterId:string;

  constructor(service:ChapterService) { 
    this._service = service;
  }

  ngOnInit(): void {
    this.getChapter();
  }
  getChapter():void{
    this._service.GetChapterBy(this.chapterId).subscribe
    (
      response=>{
        this.chapter = response;
      },
      error=>{
        alert(error);
      }
    );
  }
}
