using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using PolarisLog.Domain.QuerySide;
using Xunit;

namespace PolarisLog.Tests.Domain.QuerySide
{
    public class PagedListTest
    {
        [Fact]
        public void ToPagedList_DeveAdicionarTotalPageCurrentAndTotalPages()
        {
            var lista = new List<int> {1, 2, 3, 4, 5}.AsQueryable();
            
            var pagedList = PagedList<int>.ToPagedList(lista, 1, 20);

            pagedList.CurrentPage.Should().Be(1);
            pagedList.TotalPages.Should().Be(1);
            pagedList.PageSize.Should().Be(20);
            pagedList.TotalCount.Should().Be(lista.Count());
            pagedList.HasNext.Should().Be(false);
            pagedList.HasPrevious.Should().Be(false);
        }
        
        [Fact]
        public void ToPagedList_DeveSetarPagina1QuandoPaginaForNegativa()
        {
            var lista = new List<int> {1, 2, 3, 4, 5}.AsQueryable();
            
            var pagedList = PagedList<int>.ToPagedList(lista, -1, 20);

            pagedList.CurrentPage.Should().Be(1);
        }
    }
}