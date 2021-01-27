import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ChapterComponent } from './chapter.component';
import { ChapterService } from './chapter.service';
import {HttpClientModule} from '@angular/common/http'


@NgModule({
  declarations: [ChapterComponent],
  imports: [
    CommonModule,
    HttpClientModule
  ],
  providers:[
    ChapterService
  ],
  exports:[
    ChapterComponent
  ]
})
export class ChapterModule { }
