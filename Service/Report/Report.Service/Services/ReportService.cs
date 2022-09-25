﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Report.Service.Context;
using Report.Service.Dtos;
using Shared.Dtos;

namespace Report.Service.Services
{
    public class ReportService: IReportService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ReportService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<List<ReportDto>>> GetAllAsync()
        {
            var reports = await _context.Reports.ToListAsync();
            return Response<List<ReportDto>>.Success(_mapper.Map<List<ReportDto>>(reports), 200);

        }
        public async Task<Response<ReportDto>> GetByIdAsync(int id)
        {
            var report = await _context.Reports.FindAsync(id);

            if (report == null)
            {
                return Response<ReportDto>.Fail("Report not found!", 404);
            }

            return Response<ReportDto>.Success(_mapper.Map<ReportDto>(report), 200);
        }
        public async Task<Response<ReportDto>> Create(ReportCreateDto reportDto)
        {
            var report = _mapper.Map<Models.Report>(reportDto);
            _context.Reports.Add(report);
            await _context.SaveChangesAsync();
            return Response<ReportDto>.Success(_mapper.Map<ReportDto>(report), 200);
        }
        public async Task<Response<NoContent>> Update(ReportDto reportDto)
        {
            var report = _mapper.Map<Models.Report>(reportDto);
            _context.Entry(report).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportExists(report.Id))
                {
                    return Response<NoContent>.Fail("Report not found!", 404);
                }
                else
                {
                    throw;
                }
            }

            return Response<NoContent>.Success( 200);
        }
        public async Task<Response<NoContent>> Delete(int id)
        {
            var report = await _context.Reports.FindAsync(id);

            if (report == null)
            {
                return Response<NoContent>.Fail("Report not found!", 404);
            }
            _context.Reports.Remove(report);
            await _context.SaveChangesAsync();
            return Response<NoContent>.Success(200);
        }
        private bool ReportExists(int id)
        {
            return _context.Reports.Any(e => e.Id == id);
        }
    }
}
