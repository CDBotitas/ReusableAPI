using System.Reflection;
using ReusableCore.Abstractions; // Para conocer el gafete ICatalog

namespace ReusableAPI;

public static class ModuleCharge
{
    // Ahora recibimos también "services", que es el directorio oficial de la API
    public static void LoadModules(string contentRootPath, IServiceCollection services)
    {
        string modulesPath = Path.Combine(contentRootPath, "Modules");

        if (!Directory.Exists(modulesPath))
        {
            Directory.CreateDirectory(modulesPath);
        }

        string[] dllFiles = Directory.GetFiles(modulesPath, "*.dll");

        foreach (string file in dllFiles)
        {
            // 1. Cargamos el archivo a la memoria del salón
            Assembly assembly = Assembly.LoadFrom(file);

            // 2. Revisamos a todos los presentes buscando a los que tengan el gafete ICatalog
            var catalogTypes = assembly.GetTypes()
                .Where(t => typeof(ICatalog).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

            // 3. A los que lo tengan, los anotamos en el directorio oficial
            foreach (var type in catalogTypes)
            {
                services.AddSingleton(typeof(ICatalog), type);
            }
        }
    }
}