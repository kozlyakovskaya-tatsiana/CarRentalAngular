import { FilterArrayPipe } from './filter-countries.pipe';

describe('FilterCountriesPipe', () => {
  it('create an instance', () => {
    const pipe = new FilterArrayPipe();
    expect(pipe).toBeTruthy();
  });
});
