using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.VisualBasic;
using Radzen.Blazor.Markdown;
using Radzen.Blazor.Rendering;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Text;
using static System.Collections.Specialized.BitVector32;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GPACARICOM.Constants
{
    public static class Constants
    {
        public static string appName { get; set; } = "GPA Caricom";
        public static string whoWeAre { get; set; } = """

        <section id = "about-gpa-caribbean" class="container py-5">
        <header class="text-center mb-5">
        <h1>GPA Caribbean</h1>
        <p class="lead">
            <strong>Connecting Caribbean Youth.Empowering Leaders.Driving Innovation.Building the Future.</strong>
        </p>
        </header>

         <section class="mb-5">
        <h2>About GPA Caribbean</h2>
        <p>
            GPA Caribbean is a regional youth development and empowerment organisation dedicated to preparing the next generation of Caribbean leaders, innovators, entrepreneurs, and professionals.
        </p>

        <p>
            The organisation seeks to unlock the potential of Caribbean youth by providing transformative learning experiences, cross-border opportunities, professional development pathways, and entrepreneurship support that strengthen regional integration and economic participation.
        </p>

        <p>
            GPA Caribbean operates on the belief that the Caribbean's greatest asset is its people, particularly its young population. By connecting youth across territories and creating opportunities for skills development, mobility, innovation, and leadership, the organisation contributes to a more competitive, connected, and prosperous Caribbean.
        </p>
        </section>

         <section class="mb-5">
        <h2>Vision</h2>
        <p>
            To create a globally competitive, innovative, and interconnected Caribbean youth community that drives sustainable economic growth, regional cooperation, and social transformation.
        </p>
          </section>

         <section class="mb-5">
        <h2>Mission</h2>
        <p>
            To empower Caribbean youth and nurture professional growth through transformative training, international exchange and travel opportunities, STEM-focused internships, entrepreneurship development, and leadership programmes that foster innovation, economic participation, and meaningful people - to - people integration across the Caribbean region.
        </p>
          </section>

          <section class="mb-5">
        <h2>Core Pillars</h2>

        <!-- Pillar 1 -->
        <article class="mb-5">
            <h3>1. Youth Empowerment &amp; Professional Development</h3>

            <p>
                GPA Caribbean equips young people with the practical skills, knowledge, and confidence needed to thrive in an increasingly global economy.
            </p>

            <h4>Key Initiatives</h4>
            <ul>
                <li>Leadership development programmes</li>
                <li>Career readiness and employability training</li>
                <li>Digital skills and future-of-work competencies</li>
                <li>Professional certification opportunities</li>
                <li>Mentorship and coaching networks</li>
                <li>Public speaking and communication training</li>
            </ul>

            <h4>Expected Outcomes</h4>
            <ul>
                <li>Enhanced employability</li>
                <li>Increased workforce competitiveness</li>
                <li>Stronger leadership capacity among Caribbean youth</li>
                <li>Expanded access to career opportunities</li>
            </ul>
        </article>

        <!-- Pillar 2 -->
        <article class="mb-5">
            <h3>2. International Exchange &amp; Cross-Border Mobility</h3>

            <p>
                The organisation promotes regional and international exposure through structured exchange programmes, educational travel, and cultural immersion experiences.
            </p>

            <h4>Key Initiatives</h4>
            <ul>
                <li>Caribbean youth exchange programmes</li>
                <li>Study tours and educational travel</li>
                <li>International conferences and forums</li>
                <li>Cultural immersion experiences</li>
                <li>Regional leadership summits</li>
                <li>Volunteer and service-learning opportunities</li>
            </ul>

            <h4>Expected Outcomes</h4>
            <ul>
                <li>Greater regional understanding and cooperation</li>
                <li>Strengthened Caribbean identity</li>
                <li>Expanded professional networks</li>
                <li>Increased mobility and people integration</li>
            </ul>
        </article>

        <!-- Pillar 3 -->
        <article class="mb-5">
            <h3>3. STEM Education &amp; Internship Pathways</h3>

            <p>
                GPA Caribbean aims to build a future-ready workforce by increasing youth participation in Science, Technology, Engineering, and Mathematics (STEM).
            </p>

            <h4>Key Initiatives</h4>
            <ul>
                <li>STEM internship placements</li>
                <li>Technology and innovation bootcamps</li>
                <li>Coding and digital skills programmes</li>
                <li>Research and innovation projects</li>
                <li>Industry partnerships</li>
                <li>Artificial Intelligence and emerging technology training</li>
            </ul>

            <h4>Expected Outcomes</h4>
            <ul>
                <li>Improved STEM workforce participation</li>
                <li>Increased innovation capacity</li>
                <li>Greater access to technology careers</li>
                <li>Enhanced regional competitiveness</li>
            </ul>
        </article>

        <!-- Pillar 4 -->
        <article class="mb-5">
            <h3>4. Entrepreneurship &amp; Innovation Ecosystem</h3>

            <p>
                The organisation supports young entrepreneurs in transforming ideas into sustainable ventures that create jobs and economic value.
            </p>

            <h4>Key Initiatives</h4>
            <ul>
                <li>Startup incubators and accelerators</li>
                <li>Business development training</li>
                <li>Innovation challenges and hackathons</li>
                <li>Access to mentors and investors</li>
                <li>Financial literacy programmes</li>
                <li>Enterprise networking opportunities</li>
            </ul>

            <h4>Expected Outcomes</h4>
            <ul>
                <li>Increased youth-owned businesses</li>
                <li>Job creation</li>
                <li>Stronger entrepreneurial ecosystems</li>
                <li>Sustainable economic growth</li>
            </ul>

            <p>
                Regional entrepreneurship support aligns with broader Caribbean efforts to strengthen youth entrepreneurship and business development across the region.
            </p>
        </article>

        <!-- Pillar 5 -->
        <article class="mb-5">
            <h3>5. Regional Integration &amp; People Connectivity</h3>

            <p>
                GPA Caribbean actively supports Caribbean integration by fostering meaningful connections among young people across territories.
            </p>

            <h4>Key Initiatives</h4>
            <ul>
                <li>Regional youth networks</li>
                <li>Collaborative projects among Caribbean countries</li>
                <li>Youth ambassador programmes</li>
                <li>Policy engagement forums</li>
                <li>Community impact initiatives</li>
                <li>CARICOM-focused education and advocacy</li>
            </ul>

            <h4>Expected Outcomes</h4>
            <ul>
                <li>Stronger regional identity</li>
                <li>Increased youth participation in regional development</li>
                <li>Enhanced collaboration across Caribbean states</li>
                <li>Greater understanding of regional opportunities</li>
            </ul>

            <p>
                Regional institutions have increasingly emphasized youth participation as a critical component of Caribbean integration and development.
            </p>
        </article>
        </section>

        <section class="mb-5">
        <h2>Strategic Objectives</h2>

        <ul>
            <li>Empower youth through education, training, and leadership development.</li>
            <li>Expand access to international and regional opportunities.</li>
            <li>Increase participation in STEM and innovation sectors.</li>
            <li>Foster entrepreneurship and business creation.</li>
            <li>Promote workforce readiness and professional excellence.</li>
            <li>Strengthen regional cooperation and Caribbean identity.</li>
            <li>Facilitate meaningful youth engagement in economic development.</li>
            <li>Build partnerships among governments, educational institutions, private sector organisations, and international agencies.</li>
        </ul>
         </section>

         <section class="mb-5">
        <h2>Target Beneficiaries</h2>

        <ul>
            <li>Secondary and tertiary-level students</li>
            <li>Recent graduates</li>
            <li>Young professionals</li>
            <li>Aspiring entrepreneurs</li>
            <li>STEM students and innovators</li>
            <li>Youth leaders and community advocates</li>
            <li>Caribbean diaspora youth seeking regional engagement</li>
        </ul>
            </section>

            <section class="mb-5">
        <h2>Long-Term Impact</h2>

        <p>
            Through its programmes and partnerships, GPA Caribbean aims to develop a generation of skilled, innovative, entrepreneurial, and globally connected Caribbean citizens who can contribute meaningfully to economic growth, social development, and regional integration.
        </p>

        <p>
            The organisation envisions a Caribbean where young people can access opportunities across borders, build successful careers and businesses, and serve as active contributors to a more united, resilient, and prosperous region.
        </p>
         </section>

          <footer class="text-center mt-5">
        <blockquote>
            <strong>
                "Connecting Caribbean Youth. Empowering Leaders. Driving Innovation. Building the Future."
            </strong>
        </blockquote>
          </footer>

        </section>
       """;

    }
}
