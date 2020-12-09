export class PagedResponse<T>{
  itemsPerPage: Array<T>;
  pageNumber: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
  totalPages: number;
}
