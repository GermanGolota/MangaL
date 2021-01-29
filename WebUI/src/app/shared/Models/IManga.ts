import { IComment } from "./IComment";

export interface IManga{
  mangaTitle: string;
  desription: string;
  comments: IComment[];
  chapters: ChapterInfoModel[];
  coverPictureLocation: string;
}
export interface ChapterInfoModel {
  chapterName: string;
  chapterNumber: number;
  chapterId: string;
}