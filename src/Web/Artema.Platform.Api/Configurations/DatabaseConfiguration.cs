using FluentValidation;

namespace Artema.Platform.Api.Configurations;

public class DatabaseConfiguration
{
    public string ConnectionString { get; set; } = default!;

    private DatabaseConfiguration() { }

    public static DatabaseConfiguration BuildConfiguration(IConfiguration appConfiguration)
    {
        const string sectionName = "DatabaseConfiguration";
        
        var config = new DatabaseConfiguration();
        var section = appConfiguration.GetSection(sectionName);
        section.Bind(config);
        
        var validator = new DatabaseConfigurationValidator();
        var validation = validator.Validate(config);

        if (!validation.IsValid)
            throw new Exception($"'{sectionName}' appsettings section was not valid. Validation errors: {validation}");

        return config;
    }
}

public class DatabaseConfigurationValidator : AbstractValidator<DatabaseConfiguration>
{
    public DatabaseConfigurationValidator()
    {
        RuleFor(x => x.ConnectionString)
            .NotEmpty();
    }
}