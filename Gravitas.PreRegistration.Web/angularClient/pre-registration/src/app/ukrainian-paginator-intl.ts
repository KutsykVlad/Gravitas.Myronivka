import { MatPaginatorIntl } from '@angular/material';

const ukrainianRangeLabel = (page: number, pageSize: number, length: number) => {
  if (length === 0 || pageSize === 0) {
    return `0 із ${length}`;
  }

  length = Math.max(length, 0);

  const startIndex = page * pageSize;

  // If the start index exceeds the list length, do not try and fix the end index to the end.
  const endIndex =
    startIndex < length
      ? Math.min(startIndex + pageSize, length)
      : startIndex + pageSize;

  return `${startIndex + 1} - ${endIndex} із ${length}`;
};

export function getUkrainianPaginatorIntl() {
  const paginatorIntl = new MatPaginatorIntl();

  paginatorIntl.itemsPerPageLabel = 'Елементів на сторінці:';
  paginatorIntl.nextPageLabel = 'Наступна сторінка';
  paginatorIntl.previousPageLabel = 'Попередня сторінка';
  paginatorIntl.getRangeLabel = ukrainianRangeLabel;

  return paginatorIntl;
}
